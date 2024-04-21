using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services.Consortium
{
    public class ConsortiumGenerateLogicService
    {
        private List<FloorDepartmentDTO> floorDepartmentDTOs = new List<FloorDepartmentDTO>();

        public ConsortiumGenerateLogicService()
        {

        }


        public List<FloorDepartmentDTO> GetStructureTower(TowerConfig towerConfig)
        {
            List<CountDeparmentsByFloor> countDeparmentsByFloor = towerConfig.CountDeparmentsByFloors;

            towerConfig.Floors = towerConfig.HasMezzanine ? towerConfig.Floors + 1 : towerConfig.Floors;
            towerConfig.Floors = towerConfig.HasLowLevel ? towerConfig.Floors + 1 : towerConfig.Floors;


            if (towerConfig.FloorConfig.Nomencalture.Equals(NomencaltureEnum.Numeric))
            {
                if (towerConfig.DepartmentConfig.Nomencalture.Equals(NomencaltureEnum.Alphanumeric))
                {
                    floorDepartmentDTOs.AddRange(GenerateAlphanumericDepartments(towerConfig.Floors, countDeparmentsByFloor));
                }

                if (towerConfig.DepartmentConfig.Nomencalture.Equals(NomencaltureEnum.Numeric))
                {
                    LogicGenrationDTO logicGenrationDTO = new LogicGenrationDTO
                    {
                        Floors = towerConfig.Floors,
                        CountDepartmentsByFloor = countDeparmentsByFloor,
                        Iteration = towerConfig.DepartmentConfig.Iteration,
                        Sequential = towerConfig.DepartmentConfig.Sequential,
                    };

                    floorDepartmentDTOs.AddRange(GenerateNumericDepartments(logicGenrationDTO));
                }
            }

            /* if (towerConfig.FloorConfig.Nomencalture.Equals(NomencaltureEnum.Alphanumeric))
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
             }*/

            return floorDepartmentDTOs;
        }


        private List<FloorDepartmentDTO> GenerateAlphanumericDepartments(int numFloors, List<CountDeparmentsByFloor> countDeparmentsByFloors)
        {
            List<FloorDepartmentDTO> departmentDTOs = new List<FloorDepartmentDTO>();

            List<char> alphabet = ObtenerAbecedario(26);

            var countDeparmentsUniform = countDeparmentsByFloors.Count == 1 ? countDeparmentsByFloors.First().DepartmentsCount : 0;
            List<int> departmentsPerFloor = new List<int>();

            if (countDeparmentsByFloors.Count > 1)
            {
                foreach (CountDeparmentsByFloor c in countDeparmentsByFloors)
                {
                    departmentsPerFloor.Add(c.DepartmentsCount);
                }
            }

            if (countDeparmentsUniform > 0)
            {
                for (int i = 1; i <= numFloors; i++)
                {
                    for (int j = 1; j <= countDeparmentsUniform; j++)
                    {
                        FloorDepartmentDTO departmentDTO = new FloorDepartmentDTO();
                        departmentDTO.Floor = i.ToString();
                        departmentDTO.Deparment = alphabet[j - 1].ToString();
                        departmentDTOs.Add(departmentDTO);
                    }
                }
            }

            if (departmentsPerFloor.Count > 0)
            {
                int floorIndex = 0;
                foreach (int d in departmentsPerFloor)
                {
                    for (int j = 1; j <= d; j++)
                    {
                        FloorDepartmentDTO departmentDTO = new FloorDepartmentDTO();
                        departmentDTO.Floor = (floorIndex + 1).ToString();
                        departmentDTO.Deparment = alphabet[j - 1].ToString();
                        departmentDTOs.Add(departmentDTO);
                    }
                    floorIndex++;
                }
            }

            return departmentDTOs;
        }


        private List<FloorDepartmentDTO> GenerateNumericDepartments(LogicGenrationDTO logicGenrationDTO)
        {
            List<FloorDepartmentDTO> floorDTOs = new List<FloorDepartmentDTO>();

            var countDeparmentsUniform = logicGenrationDTO.CountDepartmentsByFloor.Count == 1
                ? logicGenrationDTO.CountDepartmentsByFloor.First().DepartmentsCount : 0;

            List<int> departmentsPerFloor = new List<int>();

            if (logicGenrationDTO.CountDepartmentsByFloor.Count > 1)
            {
                foreach (CountDeparmentsByFloor c in logicGenrationDTO.CountDepartmentsByFloor)
                {
                    departmentsPerFloor.Add(c.DepartmentsCount);
                }
            }


            if (countDeparmentsUniform > 0)
            {
                int totalDepartments = logicGenrationDTO.Floors.Value * countDeparmentsUniform;
                int sequentialDepartments = totalDepartments;

                if (logicGenrationDTO.Sequential)
                {
                    for (int i = 1; i <= sequentialDepartments; i++)
                    {
                        FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                        int floorNumber = (int)Math.Ceiling((double)i / countDeparmentsUniform);
                        floorDTO.Floor = floorNumber.ToString();
                        floorDTO.Deparment = i.ToString();
                        floorDTOs.Add(floorDTO);
                    }
                }
                else
                {
                    for (int i = 1; i <= logicGenrationDTO.Floors; i++)
                    {
                        for (int j = 1; j <= countDeparmentsUniform; j++)
                        {
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                            floorDTO.Floor = i.ToString();

                            int departmentNumber = i * logicGenrationDTO.Iteration.Value + j;
                            floorDTO.Deparment = departmentNumber.ToString();

                            floorDTOs.Add(floorDTO);
                        }
                    }
                }
            }


            if (departmentsPerFloor.Count > 0)
            {
                if (logicGenrationDTO.Sequential)
                {
                    int floorIndex = 1;
                    int departmentIndex = 1;

                    foreach (int departmentsCount in departmentsPerFloor)
                    {
                        for (int i = 0; i < departmentsCount; i++)
                        {
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                            floorDTO.Floor = floorIndex.ToString();
                            floorDTO.Deparment = departmentIndex.ToString();
                            floorDTOs.Add(floorDTO);

                            departmentIndex++;
                        }

                        floorIndex++;
                    }
                }
                else
                {
                    int floorIndex = 1;
                    foreach (int departmentsCount in departmentsPerFloor)
                    {
                        for (int j = 1; j <= departmentsCount; j++)
                        {
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                            floorDTO.Floor = floorIndex.ToString();

                            int departmentNumber = floorIndex * logicGenrationDTO.Iteration.Value + j;
                            floorDTO.Deparment = departmentNumber.ToString();

                            floorDTOs.Add(floorDTO);
                        }
                        floorIndex++;
                    }
                }
            }

            return floorDTOs;
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


        /*private List<FloorDepartmentDTO> GenerateAlphanumericFloors(LogicGenrationDTO logicGenrationDTO)
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
        }*/


        /*private List<FloorDepartmentDTO> GenerateAlphanumericDepartments(int numFloors, List<char> alphabet)
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
        }*/


    }
}
