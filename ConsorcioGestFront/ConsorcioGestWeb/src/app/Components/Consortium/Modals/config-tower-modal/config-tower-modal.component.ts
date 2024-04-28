import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigGridComponent } from '../config-grid/config-grid.component';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { Tower } from 'src/app/Models/Models/ConsortiumConfigModel';

@Component({
  selector: 'app-config-tower-modal',
  templateUrl: './config-tower-modal.component.html',
  styleUrls: ['./config-tower-modal.component.css']
})
export class ConfigTowerModalComponent {

  _TowerIsPresent: boolean = true;
  pisos:number = 0;
  tower:Tower = new Tower();
  depsByFloor:number = 0;

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


  closeModal(){
    this._TowerIsPresent = false
  }
  
  save(){
   
    if(!this.listTowerConfig.find(tower => tower.name === this.tower.name)){     
      this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:this.depsByFloor});
      this.listTowerConfig.push(this.tower);
    }
    else{
      var index = this.listTowerConfig.findIndex(tower => tower.name === this.tower.name);
      this.tower.towerConfig.countDeparmentsByFloors.push({departmentsCount:this.depsByFloor});
      this.listTowerConfig[index] = this.tower
    }
    console.log(this.listTowerConfig);    
    this.towerConfigShared.setListTower(this.listTowerConfig);
    this.closeModal();
  }

  showGridConfig(){
    this.towerConfigShared.setFloor(this.tower.towerConfig.floors);
    this.modalService.open(ConfigGridComponent);
  }

  //VALIDACIONES 
  onCheckboxChange(checkboxNumber: number) {
    if (checkboxNumber === 1) {
      if (this.tower.towerConfig.isUniform) {
        this.tower.towerConfig.isUniqual = false;
      }
    } else if (checkboxNumber === 2) {   
      if (this.tower.towerConfig.isUniqual) {
        this.tower.towerConfig.isUniform = false;
      }
    }
  }
}
