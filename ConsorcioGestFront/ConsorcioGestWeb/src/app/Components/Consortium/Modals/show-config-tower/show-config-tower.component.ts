import { Component, ElementRef, Input, ViewChild } from '@angular/core';
@Component({
  selector: 'app-show-config-tower',
  templateUrl: './show-config-tower.component.html',
  styleUrls: ['./show-config-tower.component.css']
})
export class ShowConfigTowerComponent {

  @Input() departmentList: any[] = [];
  @ViewChild('tableRef') tableRef: ElementRef = new ElementRef(null);
  floors: string[] = [];
  departments: string[] = [];

  constructor() {

  }
  see(){
    console.log(this.departmentList);
    const table = this.tableRef.nativeElement;
    const departments: any[] = this.readTableData(table);
    console.log(departments);
  }

  obtenerPisosUnicos(departamentos: any[]): string[] {
    const pisosUnicosSet = new Set<string>();
    departamentos.forEach(departamento => {
        pisosUnicosSet.add(departamento.floor);
    });
    const pisosUnicosArray = Array.from(pisosUnicosSet);  
    return pisosUnicosArray;
  }
  obtenerDepartamentosPorPiso(floor: string): any[] {
    return this.departmentList.filter(department => department.floor === floor); 
  }
  
  readTableData(table: HTMLTableElement): any[] {
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



  
}
