export class TowerConfig {
    floors: number;
    departmentConfig: DepartmentConfig;
    isUniform: boolean;
    isUniqual: boolean;
    hasLowLevel: boolean;
    countDepartmentsByFloors: CountDepartmentsByFloor[];
  
    constructor() {
      this.floors = 15;
      this.departmentConfig = new DepartmentConfig();
      this.isUniform = false;
      this.isUniqual = false;
      this.hasLowLevel = false;
      this.countDepartmentsByFloors = [];
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
    floor: number | null;
    departmentsCount: number;
  
    constructor() {
      this.floor = null;
      this.departmentsCount = 0;
    }
  }
  
  export enum NomencaltureEnum {
    Numeric = 1,
    Alphanumeric = 2
  }
  