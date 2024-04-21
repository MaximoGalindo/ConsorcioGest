using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services.Consortium
{
    public class ConsortiumService
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

    }
}
