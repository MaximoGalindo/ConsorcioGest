

import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigTowerModalComponent } from '../Modals/config-tower-modal/config-tower-modal.component';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { CommonSpaceConfig, CommonSpacesModel, ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { Router } from '@angular/router';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';

@Component({
  selector: 'app-register-consortium',
  templateUrl: './register-consortium.component.html',
  styleUrls: ['./register-consortium.component.css']
})
export class RegisterConsortiumComponent {

  _ShowConfigTowerModal: boolean = false;
  _ShowCommonSpaceConfigModal: boolean = false;
  SelectedTower:Tower = new Tower();
  SelectedCommonSpace:CommonSpaceConfig = new CommonSpaceConfig();

  selectedTab: number = 3;
  consortiumConfig: ConsortiumConfiguration = new ConsortiumConfiguration();
  towerList:Tower[] = []; 
  commonSpacesList:ListItemDTO[] = []
  commonSpacesConfigs:CommonSpaceConfig[] = [];

  constructor(
    private towerConfigShared:ConsortiumConfigSharedService,
    private router:Router,
    private consortiumService:ConsortiumService
  ){

  }

  ngOnInit(){
    this.consortiumService.GetCommonSpaces().subscribe({
      next: data => {
        this.commonSpacesList = data
      }
    })
  }

  //MODAL
  ConfigureTower(item:Tower){
    this._ShowConfigTowerModal = true;
    this.SelectedTower = item;
  }

  CloseConfigTowerModal(){
    this._ShowConfigTowerModal = false;
  }
  CloseCommonSpaceModal(){
    this._ShowCommonSpaceConfigModal = false;
  }

  ShowCommonSpaceConfigModal(commonSpace:any){
    this.SelectedCommonSpace = new CommonSpaceConfig();
    this.SelectedCommonSpace.idSpace = commonSpace.id;
    this.SelectedCommonSpace.nameSpace = commonSpace.name;
    this._ShowCommonSpaceConfigModal = true;
  }
   //FORMULARIO

  AddTower(){
    this.towerList.push(new Tower());
  }
  RemoveTower(index:number){
    this.towerList.splice(index, 1);
  }

  selectedIndices: Set<number> = new Set<number>();

  onCheckboxChange(index: number) {
    if (this.selectedIndices.has(index)) {
      this.selectedIndices.delete(index); // Deseleccionar el checkbox si ya está seleccionado
    } else {
      this.selectedIndices.add(index); // Seleccionar el checkbox
    }
  }

  isSelected(index: number): boolean {
    return this.selectedIndices.has(index);
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
    this.towerConfigShared.CommonSpaceList$.subscribe({
      next: consortiumConfig => {
        this.commonSpacesConfigs = consortiumConfig
      }
    })
    this.consortiumConfig.Towers = this.towerList;
    this.consortiumConfig.CommonSpaces = this.commonSpacesConfigs    
    this.towerConfigShared.setConsortiumConfig(this.consortiumConfig)
    this.router.navigate(['/register-consortium/confirm']);
    console.log(this.consortiumConfig);  
  }
}