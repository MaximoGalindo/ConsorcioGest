import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { ConsortiumConfiguration, FloorDeparmentDTO, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { CountDepartmentsByFloor, DepartmentConfig, NomencaltureEnum, TowerConfig } from 'src/app/Models/Models/TowerConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';
@Component({
  selector: 'app-show-config-tower',
  templateUrl: './show-config-tower.component.html',
  styleUrls: ['./show-config-tower.component.css']
})
export class ShowConfigTowerComponent {

  @Output() _IsEdit = new EventEmitter<boolean>();
  @Output() _TableData =  new EventEmitter<any[]>();

  @ViewChild('tableRef') tableRef: ElementRef = new ElementRef(null);

  @Input() tower:Tower = new Tower();

  tableDisabled:boolean = false;
  consortiumConfig:ConsortiumConfiguration = new ConsortiumConfiguration();

  ngOnInit(){   
    if(this.tower.floorDepartment.length == 0){
      this.tower.floorDepartment = [
        { floor: "1", department: "1" },
        { floor: "1", department: "2" },
        { floor: "1", department: "3" },
        { floor: "1", department: "4" },
        { floor: "1", department: "5" },
        { floor: "2", department: "6" },
        { floor: "2", department: "7" },
        { floor: "2", department: "8" },
        { floor: "2", department: "9" },
        { floor: "2", department: "10" },
        { floor: "3", department: "11" },
        { floor: "3", department: "12" },
        { floor: "3", department: "13" },
        { floor: "3", department: "14" },
        { floor: "3", department: "15" },
        { floor: "4", department: "16" },
        { floor: "4", department: "17" },
        { floor: "4", department: "18" },
        { floor: "4", department: "19" },
        { floor: "4", department: "20" },
        { floor: "5", department: "21" },
        { floor: "5", department: "22" },
        { floor: "5", department: "23" },
        { floor: "5", department: "24" },
        { floor: "5", department: "25" }
      ];  
    }
    
  }

  constructor(
    private towerConfigShared: ConsortiumConfigSharedService
  ) {    
  }
  ObtenerPisosUnicos(departments: any[]): string[] {
    const uniqueFloors = [...new Set(departments.map(d => d.floor))];
    return uniqueFloors;
  }

  ObtenerDepartamentosPorPiso(floor: string): any[] {
    return this.tower.floorDepartment.filter(d => d.floor === floor);
  }

  EmitDataTable(): void {
    if (this.tableRef) {
      const table: HTMLTableElement = this.tableRef.nativeElement;
      setTimeout(() => {
        const departments: FloorDeparmentDTO[] = this.ReadTableData(table);
        this._TableData.emit(departments);
      },1000)

    }
  }

  ReadTableData(table: HTMLTableElement): FloorDeparmentDTO[] {
    const departments: FloorDeparmentDTO[] = [];
    const rows = table.rows;

    for (let i = 1; i < rows.length; i++) {
      const floorCell = rows[i].cells[0].querySelector('input');
      const floor = floorCell ? floorCell.value.trim() : '';
  
      for (let j = 1; j < rows[i].cells.length; j++) {
        const departmentInput = rows[i].cells[j].querySelector('input');
        
        if (departmentInput) {
          const department = departmentInput.value.trim();
  
          if (department !== '') { // Check if department is not empty
            departments.push({ floor, department });
          }
        }
      }
    }
    return departments;
  }

}
