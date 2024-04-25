import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigGridComponent } from '../config-grid/config-grid.component';
import { TowerConfigSharedService } from 'src/app/Services/Shared/tower-config-shared.service';
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

  listTowerConfig:Tower[] = []; 
  constructor(
    private modalService: NgbModal,
    private towerConfigShared: TowerConfigSharedService
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
    if(!this.listTowerConfig.find(tower => tower.Name === this.tower.Name)){
      this.listTowerConfig.push(this.tower);
    }
    else{
      var index = this.listTowerConfig.findIndex(tower => tower.Name === this.tower.Name);
      this.listTowerConfig[index] = this.tower
    }
    console.log(this.listTowerConfig);    
    this.towerConfigShared.setListTower(this.listTowerConfig);
    this.closeModal();
  }

  showGridConfig(){
    this.towerConfigShared.setFloor(this.tower.TowerConfig.floors);
    this.modalService.open(ConfigGridComponent);
  }

  //VALIDACIONES 
  onCheckboxChange(checkboxNumber: number) {
    if (checkboxNumber === 1) {
      if (this.tower.TowerConfig.isUniform) {
        this.tower.TowerConfig.isUniqual = false;
      }
    } else if (checkboxNumber === 2) {   
      if (this.tower.TowerConfig.isUniqual) {
        this.tower.TowerConfig.isUniform = false;
      }
    }
  }
}
