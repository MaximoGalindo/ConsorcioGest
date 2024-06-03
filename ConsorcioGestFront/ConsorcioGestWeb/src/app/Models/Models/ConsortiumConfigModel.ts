import { TowerConfig } from "./TowerConfigModel";

export class ConsortiumConfiguration {
  CUIT: string;
  Name: string;
  Location: string;
  Towers: Tower[];
  CommonSpaces: CommonSpaces[];

  constructor() {
    this.CUIT = '';
    this.Name = '';
    this.Location = '';
    this.Towers = [];
    this.CommonSpaces = [];
  }
}

export class Tower {
  name: string;
  towerConfig: TowerConfig;
  floorDepartments: FloorDeparmentDTO[] = [];

  constructor() {
    this.name = '';
    this.towerConfig = new TowerConfig();
    this.floorDepartments = [];
  }
}


export class CommonSpaces {
  name: string;
  hourFrom: string;
  hourTo: string;

  constructor() {
    this.name = '';
    this.hourFrom = '';
    this.hourTo = '';
  }
  
}
export class CommonSpacesModel {
  id: number = 0;
  userLimit: number = 0;
  hourFrom: string = '';
  hourTo: string = '';
  numberReservationsAvailable: number = 0;
  commonSpaceID: number = 0;
  commonSpaceName: string = '';
  countReservations: number = 0;
}


export class FloorDeparmentDTO{
  floor:string = '';
  department:string = '';    
}
