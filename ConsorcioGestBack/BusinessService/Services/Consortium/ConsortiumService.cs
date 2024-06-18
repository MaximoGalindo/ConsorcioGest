using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Services.BaseService;
using DataAccess.Data;
using DataAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
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
            SaveCommonSpaces(consortiumConfig.CommonSpaces,id);

            return true;
        }

        private void SaveCommonSpaces(List<CommonSpaces> commonSpaces, int idConsortium)
        {
            foreach(CommonSpaces commonSpace in commonSpaces) 
            {
                EspacioComunConsorcio espacioComunConsorcio = new EspacioComunConsorcio 
                {
                    LimiteUsuarios = commonSpace.LimitUsers,
                    HoraDesde = commonSpace.HourFrom,
                    HoraHasta = commonSpace.HourTo,
                    IdConsorcio = idConsortium,
                    IdEspacioComun = commonSpace.IdSpace
                };
                espacioComunConsorcio.CantidadReservasDisponibles = GetNumberHoursAvaible(commonSpace.HourFrom, commonSpace.HourTo, "HH:mm");
                DBAdd(espacioComunConsorcio, _context);
            }
        }

        public static int GetNumberHoursAvaible(string hour1, string hour2, string format)
        {
            DateTime parsedDateTime1, parsedDateTime2;

            DateTime.TryParseExact(hour1, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime1);
            DateTime.TryParseExact(hour2, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime2);

            DateTime today = DateTime.Today;

            DateTime dateTime1 = new DateTime(today.Year, today.Month, today.Day, parsedDateTime1.Hour, parsedDateTime1.Minute, 0);
            DateTime dateTime2 = new DateTime(today.Year, today.Month, today.Day, parsedDateTime2.Hour, parsedDateTime2.Minute, 0);


            TimeSpan difference = dateTime2 - dateTime1;
            double hoursDifference = difference.TotalHours;

            return Math.Abs((int)hoursDifference) + 1;
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

       /* public bool DeleteConsortium(int consortiumID)
        {
            var consortium = _context.Consorcios.Where(c => c.Id == consortiumID).FirstOrDefault();
            var contacts = _context.Contactos.Where(c => c.IdConsorcio == consortiumID).ToList();
            var consortiumConfigurations = _context.ConsortiumConfigurations.Where(c => c.IdConsortium == consortiumID).FirstOrDefault();
            var commonSpacesConsortium = _context.EspacioComunConsorcios.Where(e => e.IdConsorcio == consortiumID).ToList();
            var condominiums = _context.Condominios.Where(c => c.IdConsorcio == consortiumID).ToList();
            
            DBDelete()

            //var consortiumUsers = _context.ConsorcioUsuarios.
        }*/

        public List<ListItemDTO> GetTowers()
        {
            return _context.Condominios
                        .Where(c => c.IdConsorcio == LoginService.CurrentConsortium.Id)
                        .GroupBy(c => c.Torre)
                        .Select(group => new ListItemDTO
                        {
                            ID = group.First().Id,
                            Name = group.Key,
                        })
                        .ToList();
        }

        public List<ListItemDTO> GetCondominiums(string Tower)
        {
            return _context.Condominios
                        .Where(c => c.IdConsorcio == LoginService.CurrentConsortium.Id
                                && c.Torre == Tower)
                        .Select(c => new ListItemDTO
                        {
                            ID = c.Id,
                            Name = c.NumeroDepartamento,
                        })
                        .ToList();
        }

        public List<ListItemDTO> GetCommonSpaces()
        {
            return _context.EspacioComuns
                .Select(e => new ListItemDTO
                {
                    ID = e.Id,
                    Name = e.Nombre
                })
                .ToList();
        }
    }
}
