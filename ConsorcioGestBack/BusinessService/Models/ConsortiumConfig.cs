using BusinessService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ConsortiumConfiguration 
    { 
        public List<Tower> Towers {  get; set; }
        public List<CommonSpaces> CommonSpaces { get; set; }
    }

    public class Tower
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public TowerConfig TowerConfig { get; set; }
    }

    public class TowerConfig
    {
        public FloorConfig FloorConfig { get; set; }
        public DepartmentConfig DepartmentConfig { get; set; }
        public bool IsUniform { get; set; }
        public bool IsUniqual { get; set; }
        
        public List<CountDeparmentsByFloor> CountDeparmentsByFloors { get; set; } = null;
    }

    public class FloorConfig
    {
        public int? Iteration { get; set; }
        public NomencaltureEnum Nomencalture { get; set; }
    }
    public class DepartmentConfig
    {
        public int? Iteration { get; set; }
        public NomencaltureEnum Nomencalture { get; set; }
        public bool Sequential { get; set; }
    }
    public class CountDeparmentsByFloor
    {
        public int? Floor { get; set; } 
        public int DepartmentsCount { get; set; }
    }

    public class CommonSpaces
    {
        public string Name { get; set; }
    }


    public class FloorDepartmentDTO {
        public string Floor {  get; set; }
        public string Deparment { get; set; }
    }


}
