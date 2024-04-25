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
  Name: string;
  TowerConfig: TowerConfig;

  constructor() {
    this.Name = '';
    this.TowerConfig = new TowerConfig();
  }
}


export class CommonSpaces {
  Name: string;
  HourFrom: string;
  HourTo: string;

  constructor() {
    this.Name = '';
    this.HourFrom = '';
    this.HourTo = '';
  }
}

