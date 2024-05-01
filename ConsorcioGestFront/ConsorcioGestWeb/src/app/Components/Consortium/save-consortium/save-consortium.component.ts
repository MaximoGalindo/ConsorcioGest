import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonSpaces, ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { ShowConfigTowerComponent } from '../Modals/show-config-tower/show-config-tower.component';
import { CountDepartmentsByFloor, NomencaltureEnum } from 'src/app/Models/Models/TowerConfigModel';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { elementAt, isEmpty } from 'rxjs';

@Component({
  selector: 'app-save-consortium',
  templateUrl: './save-consortium.component.html',
  styleUrls: ['./save-consortium.component.css']
})
export class SaveConsortiumComponent {

  _OpenShowConfigModal: boolean = false
  _IsEdit: boolean = false

  consortiumConfig: ConsortiumConfiguration = new ConsortiumConfiguration()
  selectedTower:Tower = new Tower();

  constructor(
    private towerConfigShared: ConsortiumConfigSharedService,
    private modalService: NgbModal,
    private consortiumSevice: ConsortiumService
  ) {

   /* this.consortiumConfig.CUIT = '1234567890';
    this.consortiumConfig.Name = 'Gamma';
    this.consortiumConfig.Location = 'Circunvalacion';
  
    const tower1 = new Tower();
    tower1.name = 'Torre 1';
    tower1.towerConfig.isUniqual = true;
    tower1.towerConfig.floors = 5;
    tower1.towerConfig.departmentConfig.nomencalture = NomencaltureEnum.Alphanumeric;
    tower1.towerConfig.hasLowLevel = true;
    

    const tower2 = new Tower();
    tower2.name = 'Torre 2';
    tower2.towerConfig.isUniform = true;
    tower2.towerConfig.floors = 5;
    tower2.towerConfig.departmentConfig.nomencalture = NomencaltureEnum.Numeric;
    tower2.towerConfig.departmentConfig.sequential = true;
    tower2.towerConfig.hasLowLevel = false;
    const departmentPerFloort2 = new CountDepartmentsByFloor();
    departmentPerFloort2.departmentsCount = 5;
    tower2.towerConfig.countDeparmentsByFloors.push(departmentPerFloort2)


    const departmentPerFloor = new CountDepartmentsByFloor();
    const departmentPerFloor2 = new CountDepartmentsByFloor();
    const departmentPerFloor3 = new CountDepartmentsByFloor();
    const departmentPerFloor4 = new CountDepartmentsByFloor();
    const departmentPerFloor5 = new CountDepartmentsByFloor();
    departmentPerFloor.departmentsCount = 7;
    departmentPerFloor2.departmentsCount = 4;
    departmentPerFloor3.departmentsCount = 5;
    departmentPerFloor4.departmentsCount = 6;
    departmentPerFloor5.departmentsCount = 2;
    tower1.towerConfig.countDeparmentsByFloors.push(departmentPerFloor)
    tower1.towerConfig.countDeparmentsByFloors.push(departmentPerFloor2)
    tower1.towerConfig.countDeparmentsByFloors.push(departmentPerFloor3)
    tower1.towerConfig.countDeparmentsByFloors.push(departmentPerFloor4)
    tower1.towerConfig.countDeparmentsByFloors.push(departmentPerFloor5)
  
    const commonSpace1 = new CommonSpaces();
    commonSpace1.name = 'Espacio común 1';
    commonSpace1.hourFrom = '09:00';
    commonSpace1.hourTo = '18:00';
  
    this.consortiumConfig.Towers.push(tower1);
    this.consortiumConfig.Towers.push(tower2);
    this.consortiumConfig.CommonSpaces.push(commonSpace1);
    
    //BORRAR ESTO DESPUES
    towerConfigShared.setConsortiumConfig(this.consortiumConfig);*/

    towerConfigShared.ConsortiumConfig$.subscribe({
      next: consortiumConfig => {
        this.consortiumConfig = consortiumConfig
      }
    })

   }


  ShowConfigTower(tower:Tower){
    console.log(tower);
    
    if(tower.floorDepartments.length > 0){
      this.selectedTower = tower;      
      this._OpenShowConfigModal = true;     
    }
    else{
      this.consortiumSevice.GenerateLogicConfiguration(tower).subscribe({
        next: config => {   
          for(var item of config){ 
            tower.floorDepartments.push(item);    
          }              
          this.selectedTower = tower;
          this._OpenShowConfigModal = true;
        }
      })  
      tower.floorDepartments = []
    }

 
  }

  CloseModal(){
    this.selectedTower = new Tower();
    this._OpenShowConfigModal = false
  }


  Save() {
    this.consortiumConfig.Towers.forEach(tower => {
      if(tower.floorDepartments.length == 0){
        this.consortiumSevice.GenerateLogicConfiguration(tower).subscribe({
          next: config => {
            tower.floorDepartments = config;
          }
        })
      }      
    }) 
    console.log(this.consortiumConfig);
    
    this.consortiumSevice.SaveConsortium(this.consortiumConfig).subscribe({
      next: consortium => {
        console.log(consortium);
      }
    });
  }
  

  IsEdit(evento: boolean){
    this._IsEdit = evento
    return this._IsEdit;
  }
















  
}
