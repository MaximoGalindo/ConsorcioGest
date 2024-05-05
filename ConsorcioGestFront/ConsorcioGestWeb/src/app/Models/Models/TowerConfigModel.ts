export class TowerConfig {
    floors: number;
    departmentConfig: DepartmentConfig;
    isUniform: boolean;
    isUniqual: boolean;
    hasLowLevel: boolean;
    countDeparmentsByFloors: CountDepartmentsByFloor[];
  
    constructor() {
      this.floors = 0;
      this.departmentConfig = new DepartmentConfig();
      this.isUniform = false;
      this.isUniqual = false;
      this.hasLowLevel = false;
      this.countDeparmentsByFloors = [];
    }
  }
  
  export class DepartmentConfig {
    iteration: number | null;
    nomencalture: NomencaltureEnum;
    sequential: boolean;
  
    constructor() {
      this.iteration = null;
      this.nomencalture = NomencaltureEnum.Numeric; 
      this.sequential = false;
    }   
  }
  
  export class CountDepartmentsByFloor {
    departmentsCount: number;
  
    constructor() {
      this.departmentsCount = 0;
    }
  }
  
  export enum NomencaltureEnum {
    Numeric = 1,
    Alphanumeric = 2
  }
  