using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class ConsortiumService
    {
        private List<FloorDepartmentDTO> floorDepartmentDTOs = new List<FloorDepartmentDTO>();

        public ConsortiumService()
        {

        }

        public List<FloorDepartmentDTO> GenerateLogicDepartments(Tower configTower)
        {
            if (configTower.TowerConfig.IsUniform)
            {
                floorDepartmentDTOs = IsUniformStructure(configTower.TowerConfig, configTower.Floor);
            }

            if (configTower.TowerConfig.IsUniqual)
            {
                floorDepartmentDTOs = IsUniqualStructure(configTower.TowerConfig);
            }

            return floorDepartmentDTOs;
        }

        private List<FloorDepartmentDTO> IsUniformStructure(TowerConfig towerConfig, int floors)
        {
            int departments = towerConfig.CountDeparmentsByFloors.First().DepartmentsCount;            

            if (towerConfig.FloorConfig.Nomencalture.Equals(NomencaltureEnum.Alphanumeric))
            {
                List<char> alphabet = ObtenerAbecedario(floors);

                LogicGenrationDTO logicGenrationDTO = new LogicGenrationDTO
                {
                    Floors = floors,
                    CountDepartments = departments,
                    Alphabet = alphabet,
                    Iteration = towerConfig.DepartmentConfig.Iteration,
                    Sequential = towerConfig.DepartmentConfig.Sequential,
                };

                floorDepartmentDTOs.AddRange(GenerateAlphanumericFloors(logicGenrationDTO));
            }

            if (towerConfig.DepartmentConfig.Nomencalture.Equals(NomencaltureEnum.Alphanumeric))
            {
                List<char> alphabet = ObtenerAbecedario(departments);
                floorDepartmentDTOs.AddRange(GenerateAlphanumericDepartments(floors, alphabet));
            }

            return floorDepartmentDTOs;
        }

        private List<FloorDepartmentDTO> IsUniqualStructure(TowerConfig configTower)
        {
            return null;
        }


        private List<FloorDepartmentDTO> GenerateAlphanumericFloors(LogicGenrationDTO logicGenrationDTO)
        {
            List<FloorDepartmentDTO> floorDTOs = new List<FloorDepartmentDTO>();

            int totalDepartments = logicGenrationDTO.Floors.Value * logicGenrationDTO.CountDepartments.Value;
            int departmentsPerFloor = logicGenrationDTO.CountDepartments.Value;

            int sequentialDepartments = totalDepartments;
            int sequentialFloor = (int)Math.Ceiling((double)sequentialDepartments / departmentsPerFloor);

            if(logicGenrationDTO.Sequential)

            for (int i = 1; i <= sequentialDepartments; i++)
            {
                FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                int floorNumber = (int)Math.Ceiling((double)i / departmentsPerFloor);
                floorDTO.Floor = logicGenrationDTO.Alphabet[floorNumber - 1].ToString();
                floorDTO.Deparment = i.ToString();
                floorDTOs.Add(floorDTO);
            }
            else
            {
                for (int i = 1; i <= logicGenrationDTO.Floors; i++)
                {
                    for (int j = 1; j <= logicGenrationDTO.CountDepartments; j++)
                    {
                        FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                        floorDTO.Floor = logicGenrationDTO.Alphabet[i - 1].ToString();

                        int departmentNumber = (i * logicGenrationDTO.Iteration.Value) + j;
                        floorDTO.Deparment = departmentNumber.ToString();

                        floorDTOs.Add(floorDTO);
                    }
                }
            }

            return floorDTOs;
        }


        private List<FloorDepartmentDTO> GenerateAlphanumericDepartments(int numFloors, List<char> alphabet)
        {
            List<FloorDepartmentDTO> departmentDTOs = new List<FloorDepartmentDTO>();
            for (int i = 1; i <= numFloors; i++)
            {
                foreach (char c in alphabet)
                {
                    FloorDepartmentDTO departmentDTO = new FloorDepartmentDTO();
                    departmentDTO.Floor = i.ToString();
                    departmentDTO.Deparment = c.ToString();
                    departmentDTOs.Add(departmentDTO);
                }
            }
            return departmentDTOs;
        }

        private List<char> ObtenerAbecedario(int limite)
        {
            List<char> abecedario = new List<char>();
            for (char letra = 'A'; letra < 'A' + limite; letra++)
            {
                abecedario.Add(letra);
            }
            return abecedario;
        }


    }
}
