import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';

@Injectable({
  providedIn: 'root'
})
export class ConsortiumConfigSharedService {

  private floorTower = new BehaviorSubject<any>(null);
  FloorTower$ = this.floorTower.asObservable();

  private departmentPerFloor = new BehaviorSubject<any>(null);
  DepartmentPerFloor$ = this.departmentPerFloor.asObservable();

  private towerConfig = new BehaviorSubject<any>(null);
  TowerConfig$ = this.towerConfig.asObservable();

  private tower = new BehaviorSubject<any>(null);
  Tower$ = this.tower.asObservable();

  private towerList = new BehaviorSubject<any>(null);
  TowerList$ = this.towerList.asObservable();

  private consortiumConfig = new BehaviorSubject<any>(null);
  ConsortiumConfig$ = this.consortiumConfig.asObservable();

  setFloor(data:number){
    this.floorTower.next(data);
  }

  setDeparmentPerFloor(data:number[]){
    this.departmentPerFloor.next(data); 
  }

  setTowerConfig(data:TowerConfig[]){
    this.towerConfig.next(data);
  }

  setTower(data:Tower){
    this.tower.next(data);  
  } 

  setListTower(data:Tower[]){
    this.towerList.next(data);
  }

  setConsortiumConfig(data:ConsortiumConfiguration){
    this.consortiumConfig.next(data);
  }
  constructor() { }
}
