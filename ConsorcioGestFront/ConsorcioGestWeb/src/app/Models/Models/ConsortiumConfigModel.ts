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

  constructor() {
    this.name = '';
    this.towerConfig = new TowerConfig();
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

