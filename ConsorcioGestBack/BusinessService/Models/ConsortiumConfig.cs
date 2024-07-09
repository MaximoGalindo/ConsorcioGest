using BusinessService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ConsortiumConfig
    {
        public string CUIT {  get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Tower> Towers {  get; set; }
        public List<CommonSpaces> CommonSpaces { get; set; }
    }

    public class Tower
    {
        public string Name { get; set; }
        public TowerConfig TowerConfig { get; set; }
        public List<FloorDepartmentDTO>? FloorDepartment { get; set; }
    }

    public class TowerConfig
    {
        public int Floors { get; set; }
        public DepartmentConfig DepartmentConfig { get; set; }
        public bool IsUniform { get; set; }
        public bool IsUniqual { get; set; }
        public bool HasLowLevel { get; set; }
        public List<CountDeparmentsByFloor> CountDeparmentsByFloors { get; set; } = null;
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
        public int IdSpace { get; set; }
        public string NameSpace { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public int LimitUsers { get; set; }
    }

    public class FloorDepartmentDTO {
        public string Floor {  get; set; }
        public string Department { get; set; }
    }

    public class ConsortiumDTO 
    {
        public string CUIT { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<string> Towers { get; set; }
        public List<CommonSpaces> CommonSpaces { get; set; }
    }

    public class EditConsortiumDTO 
    {   
        public int ConsortiumID { get; set; }
        public string Name { get; set; }
        public List<CommonSpaces> CommonSpaces { get; set; }
    }

}
