import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigGridComponent } from '../config-grid/config-grid.component';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { CountDepartmentsByFloor, TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { Tower } from 'src/app/Models/Models/ConsortiumConfigModel';

@Component({
  selector: 'app-config-tower-modal',
  templateUrl: './config-tower-modal.component.html',
  styleUrls: ['./config-tower-modal.component.css']
})
export class ConfigTowerModalComponent {

  @Output() _TowerIsPresent = new EventEmitter<boolean>()

  _GridIsPresent: boolean = false;
 
  @Input() tower:Tower = new Tower();

  depsByFloor:number[] = [];
  uniformDeps:number = 0;
  listTowers:Tower[] = []; 
  constructor(
    private modalService: NgbModal,
    private towerConfigShared: ConsortiumConfigSharedService
  ){
    towerConfigShared.TowerList$.subscribe({ next: towerList => {    
      if(towerList != null) this.listTowers = towerList
    }})
  }
  ngOnInit(){    
  }

  CloseModal(){
    this._TowerIsPresent.emit(false)
  }
  
  CloseGridModal(){
    this._GridIsPresent = false
  }

  save(){      
    if(!this.listTowers.find(tower => tower.name === this.tower.name)){
      if(this.uniformDeps > 0){
        this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:this.uniformDeps});
      }
      else{
        this.depsByFloor.forEach(deps =>
          this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:deps}));  
      }
      this.listTowers.push(this.tower);
    }
    else{
      var index = this.listTowers.findIndex(tower => tower.name === this.tower.name);
      if(this.uniformDeps > 0){
        this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:this.uniformDeps});
      }
      else{
        this.depsByFloor.forEach(deps =>
          this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:deps}));  
      }
      this.listTowers[index] = this.tower
    } 
    this.towerConfigShared.setListTower(this.listTowers);
    this.CloseModal();
  }

  showGridConfig(){    
    this._GridIsPresent = true;
  }
  GetDepartmentsByFloor(event:CountDepartmentsByFloor[]){
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
}
