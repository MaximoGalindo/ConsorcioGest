import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigGridComponent } from '../config-grid/config-grid.component';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { CountDepartmentsByFloor, DepartmentConfig, NomencaltureEnum, TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { Utils } from 'src/app/Helpers/Utils';

@Component({
  selector: 'app-config-tower-modal',
  templateUrl: './config-tower-modal.component.html',
  styleUrls: ['./config-tower-modal.component.css']
})
export class ConfigTowerModalComponent {

  @Output() _TowerIsPresent = new EventEmitter<boolean>()

  _GridIsPresent: boolean = false;
  _ShowConfigTower: boolean = false;


  @Input() tower: Tower = new Tower();

  depsByFloor: number[] = [];
  uniformDeps: number = 0;
  listTowers: Tower[] = [];

  constructor(
    private towerConfigShared: ConsortiumConfigSharedService,
    private consortiumService: ConsortiumService
  ) {

    
  }
  ngOnInit() {
    this.towerConfigShared.TowerList$.subscribe({
      next: towerList => {
        if (towerList != null) this.listTowers = towerList
      }
    })

    if(this.tower.towerConfig.isUniform){
      this.uniformDeps = this.tower.towerConfig.countDeparmentsByFloors[0].departmentsCount;
    }    
  }

  save() {
    if (!this.listTowers.find(tower => tower.name === this.tower.name)) {
      this.listTowers.push(this.tower);
    }
    else {
      var index = this.listTowers.findIndex(tower => tower.name === this.tower.name);
      this.listTowers[index] = this.tower
    }
    this.towerConfigShared.setListTower(this.listTowers);
    console.log(this.listTowers);
    this.CloseModal();
  }

  GenerateLogicTower() {
    this.tower.floorDepartment = [];
    this.tower.towerConfig.countDeparmentsByFloors = [];
    console.log(this.tower);

    if (this.uniformDeps > 0) {
      this.tower.towerConfig.countDeparmentsByFloors.push({ departmentsCount: this.uniformDeps });
    }
    else {
      this.depsByFloor.forEach(deps =>
        this.tower.towerConfig.countDeparmentsByFloors.push({ departmentsCount: deps }));
    }

    this.consortiumService.GenerateLogicConfiguration(this.tower).subscribe({
      next: config => { 
        Utils.success("Se genero correctamente")
        this.tower.floorDepartment = config;
        this._ShowConfigTower = true
      }
    })

  }


  //GET DATA FROM OTHERS COMPONENTS

  //SHOW CONFIG TOWER
  GetTableData($event: any[]) {
    console.log($event);
    this.tower.floorDepartment = $event;
  }

  //CONFIG GRID
  GetDepartmentsByFloor(event: CountDepartmentsByFloor[]) {
    console.log(event);
    event.forEach((value) => {
      this.depsByFloor.push(value.departmentsCount);
    })
  }



  //VALIDACIONES 
  onCheckboxChange(checkboxNumber: number) {
    if (checkboxNumber === 1) {
      if (this.tower.towerConfig.isUniform) {
        this.tower.towerConfig.isUniqual = false;
        this.tower.towerConfig.countDeparmentsByFloors = [];
      }
    } else if (checkboxNumber === 2) {
      if (this.tower.towerConfig.isUniqual) {
        this.tower.towerConfig.isUniform = false;
        this.tower.towerConfig.countDeparmentsByFloors = [];
      }
    }
  }

  minFloorRequired: boolean = false;
  ClearError(){
    this.minFloorRequired = false
  }

  //MODALES  
  CloseModal() {
    this._TowerIsPresent.emit(false)
  }

  CloseGridModal() {
    this._GridIsPresent = false
  }

  ShowGridConfig() {
    if(this.tower.towerConfig.floors == null || this.tower.towerConfig.floors == 0){
      this.minFloorRequired = true;
      return;
    }
    else{
      this.minFloorRequired = false;
      this._GridIsPresent = true;
    }

  }

}
