using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Services.BaseService;
using DataAccess.Data;
using DataAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services.Consortium
{
    public class ConsortiumService : BaseService<Consorcio>
    {
        private List<FloorDepartmentDTO> floorDepartmentDTOs = new List<FloorDepartmentDTO>();

        private readonly ConsortiumGenerateLogicService consortiumGenerateLogic;
        private readonly ConsorcioGestContext _context;

        public ConsortiumService(
            ConsortiumGenerateLogicService consortiumGenerateLogic,
            ConsorcioGestContext context)
        {
            this.consortiumGenerateLogic = consortiumGenerateLogic;
            this._context = context;
        }

        public List<FloorDepartmentDTO> GenerateLogicDepartments(Tower configTower)
        {
            floorDepartmentDTOs = consortiumGenerateLogic.GetStructureTower(configTower.TowerConfig);

            return floorDepartmentDTOs;
        }

        public List<ConsortiumModel> GetAllConsortiums()
        {
            List<ConsortiumModel> consortiumList = _context.Consorcios
                    .Select(t => new ConsortiumModel
                    {
                        Id = t.Id,
                        Name = t.Nombre,
                        Location = t.Ubicacion
                    }).ToList();

            return consortiumList;
        }

        public bool SaveConsortium(ConsortiumConfig consortiumConfig)
        {

            Consorcio consorcio = new Consorcio {
                Nombre = consortiumConfig.Name,
                Ubicacion = consortiumConfig.Location,
                Cuit = consortiumConfig.CUIT,
            }; 

            DBAdd(consorcio,_context);
            var id = consorcio.Id;

            SaveConfigurationConsortium(consortiumConfig, id);
            SaveCondominiums(consortiumConfig.Towers,id);

            return true;
        }

        private void SaveConfigurationConsortium(ConsortiumConfig consortiumConfig, int newConsortiumID)
        {        
            foreach(Tower tower in consortiumConfig.Towers)
            {
                TowerConfig towerConfig = tower.TowerConfig;
                DepartmentConfig department = towerConfig.DepartmentConfig;

                ConsortiumConfiguration consortiumConfiguration = new ConsortiumConfiguration();
                consortiumConfiguration.TowerName = tower.Name;
                consortiumConfiguration.Floors = towerConfig.Floors;
                consortiumConfiguration.IdConsortium = newConsortiumID;

                var deparmentConfiguration = department.Nomencalture.ToString() == "Alphanumeric" ? department.Nomencalture.ToString() :
                   department.Nomencalture.ToString() + "-" +
                   (department.Iteration != null ? (department.Iteration.ToString() + "-") : "") +
                   (department.Sequential ? department.Sequential.ToString() : "") ;

                consortiumConfiguration.DeparmentConfiguration = deparmentConfiguration;

                var deparmentsCountList = new List<int>(); 

                foreach (CountDeparmentsByFloor countDeparments in towerConfig.CountDeparmentsByFloors)
                {
                    deparmentsCountList.Add(countDeparments.DepartmentsCount); 
                }
                var deparmentsCountListString = string.Join(",", deparmentsCountList);


                consortiumConfiguration.CountDepartmentsByFloor = deparmentsCountListString;
                DBAdd(consortiumConfiguration, _context);                
            }           
        }

        private void SaveCondominiums(List<Tower> towers, int newConsortiumID)
        {
            foreach(Tower tower in towers)
            {
                foreach (FloorDepartmentDTO floorDepartment in tower.FloorDepartment)
                {
                    Condominio condominio = new Condominio();
                    condominio.IdConsorcio = newConsortiumID;
                    condominio.Torre = tower.Name; 
                    condominio.NumeroDepartamento = floorDepartment.Floor + "-" + floorDepartment.Department.ToString();
                    DBAdd(condominio, _context);
                }
            }
        }


    }
}
