import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
@Component({
  selector: 'app-show-config-tower',
  templateUrl: './show-config-tower.component.html',
  styleUrls: ['./show-config-tower.component.css']
})
export class ShowConfigTowerComponent {
 
  @Output() _ShowModal = new EventEmitter<boolean>();
  @Output() _IsEdit = new EventEmitter<boolean>();

  @ViewChild('tableRef') tableRef: ElementRef = new ElementRef(null);

  @Input() tower:Tower = new Tower();

  consortiumConfig:ConsortiumConfiguration = new ConsortiumConfiguration();

  constructor(
    private towerConfigShared: ConsortiumConfigSharedService
  ) {    
  }
  ObtenerPisosUnicos(departamentos: any[]): string[] {
    const pisosUnicosSet = new Set<string>();
    departamentos.forEach(departamento => {
        pisosUnicosSet.add(departamento.floor);
    });
    const pisosUnicosArray = Array.from(pisosUnicosSet);     
    return pisosUnicosArray;  
  }

  ObtenerDepartamentosPorPiso(floor: string): any[] {
   const floorDepartments = this.tower.floorDepartments.filter(department => department.floor === floor);  
   return floorDepartments
  }

  ReadTableData(table: HTMLTableElement): any[] {
    const departments: any[] = [];
    const rows = table.rows;
    for (let i = 1; i < rows.length; i++) { 
      const floor = rows[i].cells[0].querySelector('input')?.value.trim() || '';

      for (let j = 1; j < rows[i].cells.length; j++) {
        const inputField = rows[i].cells[j].querySelector('input');
        if (inputField) {
          const department = inputField.value.trim();
          if (department !== '') {
            departments.push({ floor, department });
          }
        }
      }
    }
  return departments;
  }

  CloseModal(){
    this.tableRef = new ElementRef(null);
    this.tower = new Tower();  
    this._ShowModal.emit(false);
    this._IsEdit.emit(false);
  }

  Save(){
    const table = this.tableRef.nativeElement;
    const departments: any[] = this.ReadTableData(table);
    console.log(departments);

    this.towerConfigShared.ConsortiumConfig$.subscribe({
      next: consortiumConfig => {
        this.consortiumConfig = consortiumConfig
      }
    })
    
    this.consortiumConfig.Towers.forEach(tower => {
      if(tower.name == this.tower.name)
        tower.floorDepartments = departments;
    })

    this.towerConfigShared.setConsortiumConfig(this.consortiumConfig)
    console.log(this.consortiumConfig);
    this._IsEdit.emit(true);
    this.CloseModal();
  }

  AddFloor(i: number){
    /*console.log(this.pisosUnicosArray);
    console.log(i);
    this.pisosUnicosArray.splice(i + 1, 0, " ");

  // Imprimir el array actualizado en la consola
  console.log(this.pisosUnicosArray);*/
  }
  AddDeparment(i: number){
    
  }
}
