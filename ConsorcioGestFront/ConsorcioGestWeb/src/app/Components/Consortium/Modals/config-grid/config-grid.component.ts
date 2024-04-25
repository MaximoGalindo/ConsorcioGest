import { Component } from '@angular/core';
import { TowerConfigSharedService } from 'src/app/Services/Shared/tower-config-shared.service';

@Component({
  selector: 'app-config-grid',
  templateUrl: './config-grid.component.html',
  styleUrls: ['./config-grid.component.css']
})
export class ConfigGridComponent {
  _GridIsPresent: boolean = true
  grid: any[][] = [];
  selectedPerFloor: number[] = [];
  floor:number = 0;
  currentPage: number = 1; 
  itemsPerPage: number = 10
  checksSelected: any[][] = [];

  constructor(
    private towerConfigShared: TowerConfigSharedService
  ) {

    this.getFloor();

    for (let i = 0; i < this.floor; i++) {
      this.grid[i] = [];
      for (let j = 0; j < 10; j++) {
        this.grid[i][j] = { checked: false };
      }
    }

    this.checksSelected = this.grid;
  }

  countSelected(index: number): number {
    return this.grid[index].filter(cell => cell.checked).length;
  }

  getFloor(){
    this.towerConfigShared.FloorTower$.subscribe({
      next: floor => {
        this.floor = floor
      }
    })
  }

  updateSelectedPerRow() {
    this.selectedPerFloor = Array.from({ length: this.floor }, () => 0); // Reiniciamos el arreglo
  
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = Math.min(startIndex + this.itemsPerPage, this.floor);
  
    for (let i = startIndex; i < endIndex; i++) {
      this.selectedPerFloor[i] = this.grid[i]?.filter(cell => cell.checked).length || 0;
    }
  }
  
  

  onCheckboxChange(col: number, row: number) {
    console.log(col, row);       
    this.checksSelected[col][row] = { checked: this.checksSelected[col][row].checked };
    this.updateSelectedPerRow();
    console.log(this.selectedPerFloor);    
  }

  generateNumbers(page: number): number[] {
    const start = (page - 1) * this.itemsPerPage + 1;
    const end = Math.min(start + this.itemsPerPage - 1, this.floor);
    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
  }

  save(){
    console.log(this.selectedPerFloor);
    
    //this.towerConfigShared.setDeparmentPerFloor(this.selectedPerFloor);
  }

  closeModal(){
    this._GridIsPresent = false
  }

  nextPage() {
    if ((this.currentPage * this.itemsPerPage) < this.floor) {
      this.currentPage++;
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }


}
