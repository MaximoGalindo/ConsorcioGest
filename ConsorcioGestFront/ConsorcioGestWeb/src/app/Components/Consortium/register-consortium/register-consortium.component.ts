

import { Component, Input } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigTowerModalComponent } from '../Modals/config-tower-modal/config-tower-modal.component';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { CommonSpaceConfig, CommonSpacesModel, ConsortiumConfiguration, EditConsortiumDTO, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { Utils } from 'src/app/Helpers/Utils';
import { ConfirmationService } from 'src/app/Helpers/ConfirmationModal';

@Component({
  selector: 'app-register-consortium',
  templateUrl: './register-consortium.component.html',
  styleUrls: ['./register-consortium.component.css']
})
export class RegisterConsortiumComponent {

  loading: boolean = false;

  isEdit: boolean = false;

  _ShowConfigTowerModal: boolean = false;
  _ShowCommonSpaceConfigModal: boolean = false;
  SelectedTower: Tower = new Tower();
  SelectedCommonSpace: CommonSpaceConfig = new CommonSpaceConfig();

  selectedTab: number = 1;
  consortiumConfig: ConsortiumConfiguration = new ConsortiumConfiguration();
  towerList: Tower[] = [];
  commonSpacesList: ListItemDTO[] = []
  commonSpacesConfigs: CommonSpaceConfig[] = [];
  consortiumID:number= 0;

  constructor(
    private towerConfigShared: ConsortiumConfigSharedService,
    private router: Router,
    private route: ActivatedRoute,
    private consortiumService: ConsortiumService,
    private confirmationService: ConfirmationService
  ) {

  }

  ngOnInit() {
    this.consortiumService.GetCommonSpaces().subscribe({
      next: data => {
        this.commonSpacesList = data
      }
    })
    this.route.params.subscribe((params: Params) => {
      const id = params['consortiumID'];
      if (id > 0) {
        this.consortiumID = id
        this.isEdit = true;
        this.consortiumService.GetConfigurationsByID(id).subscribe({
          next: data => {
            console.log(data);

            this.consortiumConfig.Name = data.name;
            this.consortiumConfig.Location = data.location;
            this.consortiumConfig.CUIT = data.cuit;

            for (let t of data.towers) {
              let tower = new Tower();
              tower.name = t;
              this.consortiumConfig.Towers.push(tower);
              this.towerList.push(tower);
            }
            this.consortiumConfig.CommonSpaces = data.commonSpaces  
            this.towerConfigShared.setCommonSpaceList(this.consortiumConfig.CommonSpaces)

            this.commonSpacesList.forEach(cs => {
              if(this.consortiumConfig.CommonSpaces.find((value) => value.nameSpace === cs.name)){
                this.selectedIndices.add(this.commonSpacesList.indexOf(cs))
              }
            })

          }
        })
      }

    });    
  }

  //MODAL
  ConfigureTower(item: Tower) {
    this._ShowConfigTowerModal = true;
    this.SelectedTower = item;
  }

  CloseConfigTowerModal() {
    this._ShowConfigTowerModal = false;
  }
  CloseCommonSpaceModal() {
    this._ShowCommonSpaceConfigModal = false;
  }

  ShowCommonSpaceConfigModal(commonSpace: any) {
    this.SelectedCommonSpace = new CommonSpaceConfig();
    this.SelectedCommonSpace.idSpace = commonSpace.id;
    this.SelectedCommonSpace.nameSpace = commonSpace.name;
    this._ShowCommonSpaceConfigModal = true;
  }
  //FORMULARIO

  AddTower() {
    this.towerList.push(new Tower());
  }
  RemoveTower(index: number) {
    this.towerList.splice(index, 1);
  }

  selectedIndices: Set<number> = new Set<number>();

  onCheckboxChange(index: number) {
    if (this.selectedIndices.has(index)) {
      this.selectedIndices.delete(index);
      this.consortiumConfig.CommonSpaces = this.consortiumConfig.CommonSpaces.filter((value) => value.nameSpace !== this.commonSpacesList[index].name);
      this.towerConfigShared.setCommonSpaceList(this.consortiumConfig.CommonSpaces)     
    } else {
      this.selectedIndices.add(index);
    }
  }

  isSelected(item: any, index: number): boolean {
    if (this.consortiumConfig.CommonSpaces.length > 0 && this.consortiumConfig.CommonSpaces.find((value) => value.nameSpace === item.name) && this.selectedIndices.has(index)) {
      return this.consortiumConfig.CommonSpaces.some((value) => value.nameSpace === item.name);
    } else {
      return this.selectedIndices.has(index);
    }
  }
  //


  //BOTONES 
  Back() {
    this.selectedTab = this.selectedTab > 1 ? this.selectedTab - 1 : this.selectedTab
  }
  Next() {
    this.selectedTab = this.selectedTab < 5 ? this.selectedTab + 1 : this.selectedTab
  }

  BackToSelectConsortium(){
    this.router.navigate(['/consortium']);
    this.Hide();
  }
  Confirm() {
    /*this.confirmationService.confirm('¿Estás seguro que quieres borrar esto?').then(confirm => {
      if (confirm) {
        
      }
    });*/
    this.loading = true;
    if(this.isEdit){

      this.towerConfigShared.CommonSpaceList$.subscribe({
        next: consortiumConfig => {
          this.commonSpacesConfigs = consortiumConfig
        }
      })
      var dto = new EditConsortiumDTO(); 
      dto.consortiumID = this.consortiumID,
      dto.name = this.consortiumConfig.Name,
      dto.commonSpaces = this.commonSpacesConfigs      
      console.log(dto);
      
      this.consortiumService.EditConsortium(dto).subscribe({
        next: consortium => {
          console.log(consortium);
          this.router.navigate(['/consortium']);
          Utils.success("El consorcio se actualizo correctamente")
          this.loading = false
          this.Hide();
          return;
        },
        error: error => {
          Utils.error(error.message)
          this.loading = false
          return;
        }
      })      
    }
    else{
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
  
      console.log(this.consortiumConfig);
      this.consortiumService.SaveConsortium(this.consortiumConfig).subscribe({
        next: consortium => {
          console.log(consortium);
          this.router.navigate(['/consortium']);
          Utils.success("El consorcio se guardo correctamente")
          this.loading = false
          this.Hide();
        },
        error: error => {
          Utils.error(error.message)
          this.loading = false
        }
      });
  
    }

    
  }

  Hide(){
    this.towerConfigShared.setCommonSpaceList([])
    this.towerConfigShared.setConsortiumConfig(new ConsortiumConfiguration())
    this.towerConfigShared.setListTower([])
  }
}