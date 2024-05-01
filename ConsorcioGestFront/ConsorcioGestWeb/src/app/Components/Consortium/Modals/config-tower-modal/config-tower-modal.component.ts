import { Component } from '@angular/core';
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

  _TowerIsPresent: boolean = true;
  _GridIsPresent: boolean = false;
 
  tower:Tower = new Tower();
  depsByFloor:number[] = [];
  uniformDeps:number = 0;
  listTowerConfig:Tower[] = []; 
  constructor(
    private modalService: NgbModal,
    private towerConfigShared: ConsortiumConfigSharedService
  ){
    towerConfigShared.Tower$.subscribe({ next: tower => {
      this.tower = tower
      }
    })    
    towerConfigShared.TowerList$.subscribe({ next: towerList => {    
      if(towerList != null) this.listTowerConfig = towerList
    }})

  }
  ngOnInit(){    
  }

  CloseModal(){
    this._TowerIsPresent = false
  }
  
  CloseGridModal(){
    this._GridIsPresent = false
  }

  save(){      
    console.log(this.uniformDeps);
    
    if(!this.listTowerConfig.find(tower => tower.name === this.tower.name)){
      if(this.uniformDeps > 0){
        this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:this.uniformDeps});
      }
      else{
        this.depsByFloor.forEach(deps =>
          this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:deps}));  
      }
      this.listTowerConfig.push(this.tower);
    }
    else{
      var index = this.listTowerConfig.findIndex(tower => tower.name === this.tower.name);
      if(this.uniformDeps > 0){
        this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:this.uniformDeps});
      }
      else{
        this.depsByFloor.forEach(deps =>
          this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:deps}));  
      }
      this.listTowerConfig[index] = this.tower
    }
    console.log(this.listTowerConfig);    
    this.towerConfigShared.setListTower(this.listTowerConfig);
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
