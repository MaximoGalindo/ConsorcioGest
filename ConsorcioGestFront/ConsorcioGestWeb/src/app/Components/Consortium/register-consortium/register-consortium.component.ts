

import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigTowerModalComponent } from '../Modals/config-tower-modal/config-tower-modal.component';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { CommonSpaces, ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-consortium',
  templateUrl: './register-consortium.component.html',
  styleUrls: ['./register-consortium.component.css']
})
export class RegisterConsortiumComponent {

  selectedTab: number = 1;
  form:FormGroup = new FormGroup({})
  consortiumConfig: ConsortiumConfiguration = new ConsortiumConfiguration();
  towerList:Tower[] = [];
  commonSpacesList:CommonSpaces[] = [];

  constructor(
    private modalService: NgbModal,
    private towerConfigShared:ConsortiumConfigSharedService,
    private router:Router
  ){

  }
  //MODAL
  ConfigureTower(item:Tower){
    this.modalService.open(ConfigTowerModalComponent);
    this.towerConfigShared.setTower(item);
  }

 //FORMULARIO

  AddTower(){
    this.towerList.push(new Tower());
  }
  RemoveTower(index:number){
    this.towerList.splice(index, 1);
  }

  AddCommonSpace(){
    this.commonSpacesList.push(new CommonSpaces());
  }
  RemoveCommonSpace(index:number){
    this.commonSpacesList.splice(index, 1);
  }
  //


  //BOTONES 
  Back(){
    this.selectedTab = this.selectedTab > 1   ? this.selectedTab - 1 : this.selectedTab
  }
  Next(){
    this.selectedTab = this.selectedTab < 5 ? this.selectedTab + 1 : this.selectedTab
  }
  Confirm(){
    this.towerConfigShared.TowerList$.subscribe({
      next: towerList => {
        this.towerList = towerList
      }
    })

    this.consortiumConfig.Towers = this.towerList;
    this.consortiumConfig.CommonSpaces = this.commonSpacesList    
    this.towerConfigShared.setConsortiumConfig(this.consortiumConfig)
    this.router.navigate(['/register-consortium/confirm']);
    console.log(this.consortiumConfig);  
  }
}