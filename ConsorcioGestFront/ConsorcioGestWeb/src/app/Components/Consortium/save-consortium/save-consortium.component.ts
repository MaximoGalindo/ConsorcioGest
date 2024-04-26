import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonSpaces, ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
import { ShowConfigTowerComponent } from '../Modals/show-config-tower/show-config-tower.component';
import { CountDepartmentsByFloor, NomencaltureEnum } from 'src/app/Models/Models/TowerConfigModel';
import { ConsortiumService } from 'src/app/Services/consortium.service';

@Component({
  selector: 'app-save-consortium',
  templateUrl: './save-consortium.component.html',
  styleUrls: ['./save-consortium.component.css']
})
export class SaveConsortiumComponent {

  consortiumConfig: ConsortiumConfiguration = new ConsortiumConfiguration()
  deparmentList:any[] = []

  constructor(
    private towerConfigShared: ConsortiumConfigSharedService,
    private modalService: NgbModal,
    private consortiumSevice: ConsortiumService
  ) {

    this.consortiumConfig.CUIT = '1234567890';
    this.consortiumConfig.Name = 'Nombre del consorcio';
    this.consortiumConfig.Location = 'Ubicación del consorcio';
  
    const tower1 = new Tower();
    tower1.name = 'Torre 1';
    tower1.towerConfig.isUniqual = true;
    tower1.towerConfig.floors = 5;
    tower1.towerConfig.departmentConfig.nomencalture = NomencaltureEnum.Numeric;
    tower1.towerConfig.departmentConfig.sequential = true;
    tower1.towerConfig.hasLowLevel = true;

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
    this.consortiumConfig.CommonSpaces.push(commonSpace1);


    /*towerConfigShared.ConsortiumConfig$.subscribe({
      next: consortiumConfig => {
        this.consortiumConfig = consortiumConfig
      }
    })*/

   }


  showConfigTower(tower:Tower){
    this.consortiumSevice.GenerateLogicConfiguration(tower).subscribe({
      next: config => {
        for(var item of config){
          this.deparmentList.push(item);
        }      
        const modalRef = this.modalService.open(ShowConfigTowerComponent);
        modalRef.componentInstance.departmentList = this.deparmentList;
      }
    })
  }

















  
}
