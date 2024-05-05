import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ConsortiumConfiguration, Tower } from 'src/app/Models/Models/ConsortiumConfigModel';
import { TowerConfig } from 'src/app/Models/Models/TowerConfigModel';

@Injectable({
  providedIn: 'root'
})
export class ConsortiumConfigSharedService {

  private towerList = new BehaviorSubject<any>(null);
  TowerList$ = this.towerList.asObservable();

  private consortiumConfig = new BehaviorSubject<any>(null);
  ConsortiumConfig$ = this.consortiumConfig.asObservable();

  setListTower(data:Tower[]){
    this.towerList.next(data);
  }

  setConsortiumConfig(data:ConsortiumConfiguration){
    this.consortiumConfig.next(data);
  }
  constructor() { }
}
