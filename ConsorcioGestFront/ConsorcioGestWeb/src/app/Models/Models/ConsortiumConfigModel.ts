import { TowerConfig } from "./TowerConfigModel";

export class ConsortiumConfiguration {
  CUIT: string;
  Name: string;
  Location: string;
  Towers: Tower[];
  CommonSpaces: CommonSpaceConfig[];

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
  floorDepartment: FloorDeparmentDTO[] = [];

  constructor() {
    this.name = '';
    this.towerConfig = new TowerConfig();
    this.floorDepartment = [];
  }
}

export class CommonSpaceConfig {
  idSpace: number = 0;
  nameSpace: string = '';
  hourFrom: string = '';
  hourTo: string = '';
  limitUsers: number = 0;
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
