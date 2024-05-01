import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CountDepartmentsByFloor } from 'src/app/Models/Models/TowerConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';

@Component({
  selector: 'app-config-grid',
  templateUrl: './config-grid.component.html',
  styleUrls: ['./config-grid.component.css']
})
export class ConfigGridComponent implements OnInit{

  @Output() _ShowModal = new EventEmitter<boolean>();
  @Output() _CountDeparmentsByFloors = new EventEmitter<CountDepartmentsByFloor[]>();
  @Input() Floor:number = 0;

  numbers:number[] = [];
  grid: any[][] = [];
  selectedPerFloor: number[] = [];
  currentPage: number = 1; 
  checksSelected: any[][] = [];

  constructor(
   
  ) {

  }
  ngOnInit(){
    console.log(this.Floor);
    for (let i = 0; i < this.Floor; i++) {
      this.grid[i] = [];
      for (let j = 0; j < 10; j++) {
        this.grid[i][j] = { checked: false };
      }
    }
    this.checksSelected = this.grid;
    this.generateNumbers()

  }

  countSelected(index: number): number {
    return this.grid[index].filter(cell => cell.checked).length;
  }

  updateSelectedPerRow() {
    this.selectedPerFloor = [];
    for (let i = 0; i < this.Floor; i++) {
      this.selectedPerFloor[i] = this.countSelected(i);
    }
  } 
  
  onCheckboxChange(col: number, row: number) {      
    this.checksSelected[col][row] = { checked: this.checksSelected[col][row].checked };
    this.updateSelectedPerRow();
    console.log(this.selectedPerFloor);    
  }

  generateNumbers(){
    for (let i = 1; i <= this.Floor; i++) {
      this.numbers.push(i);
    }   
  }
  save(){
    var list:CountDepartmentsByFloor[] = [];
    console.log(this.selectedPerFloor);
    this.selectedPerFloor.forEach((value) => {
      list.push({departmentsCount: value});
    })
    this._CountDeparmentsByFloors.emit(list);
    this.closeModal();    
  }

  closeModal(){
    this._ShowModal.emit(false);
  }



}
