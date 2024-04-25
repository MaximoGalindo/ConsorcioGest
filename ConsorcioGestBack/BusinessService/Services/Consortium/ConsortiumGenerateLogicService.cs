using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            if (towerConfig.DepartmentConfig.Nomencalture.Equals(NomencaltureEnum.Alphanumeric))
                {
                    LogicGenrationDTO logicGenrationDTO = new LogicGenrationDTO
                    {
                        Floors = towerConfig.Floors,
                        CountDepartmentsByFloor = countDeparmentsByFloor,
                        HasLowLevel = towerConfig.HasLowLevel,
                    };

                    floorDepartmentDTOs.AddRange(GenerateAlphanumericDepartments(logicGenrationDTO));
            }

            if (towerConfig.DepartmentConfig.Nomencalture.Equals(NomencaltureEnum.Numeric))
            {
                LogicGenrationDTO logicGenrationDTO = new LogicGenrationDTO
                {
                    Floors = towerConfig.Floors,
                    CountDepartmentsByFloor = countDeparmentsByFloor,
                    Iteration = towerConfig.DepartmentConfig.Iteration,
                    Sequential = towerConfig.DepartmentConfig.Sequential,
                    HasLowLevel = towerConfig.HasLowLevel,
                    };

                floorDepartmentDTOs.AddRange(GenerateNumericDepartments(logicGenrationDTO));
            }          

            return floorDepartmentDTOs;
        }


        private List<FloorDepartmentDTO> GenerateAlphanumericDepartments(LogicGenrationDTO logicGenrationDTO)
        {
            List<FloorDepartmentDTO> departmentDTOs = new List<FloorDepartmentDTO>();

            List<char> alphabet = ObtenerAbecedario(26);

            var countDeparmentsUniform = logicGenrationDTO.CountDepartmentsByFloor.Count == 1 ? logicGenrationDTO.CountDepartmentsByFloor.First().DepartmentsCount : 0;
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

                if (logicGenrationDTO.HasLowLevel)
                {
                    for (int j = 1; j <= countDeparmentsUniform; j++)
                    {
                        FloorDepartmentDTO departmentDTO = new FloorDepartmentDTO();
                        departmentDTO.Floor = "PB"; 
                        departmentDTO.Deparment = alphabet[j - 1].ToString();
                        departmentDTOs.Add(departmentDTO);
                    }
                    logicGenrationDTO.Floors -= 1;
                }

        

                for (int i = 1; i <= logicGenrationDTO.Floors; i++)
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
                int floorIndex = 1;
                foreach (int d in departmentsPerFloor)
                {
                    if (floorIndex == 1 && logicGenrationDTO.HasLowLevel)
                    {
                        for (int j = 1; j <= d; j++)
                        {                     
                            FloorDepartmentDTO departmentDTO = new FloorDepartmentDTO();
                            departmentDTO.Floor = "PB";
                            departmentDTO.Deparment = alphabet[j - 1].ToString();
                            departmentDTOs.Add(departmentDTO);
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= d; j++)
                        {
                            FloorDepartmentDTO departmentDTO = new FloorDepartmentDTO();

                            if (logicGenrationDTO.HasLowLevel)
                            {
                                departmentDTO.Floor = (floorIndex - 1).ToString();
                                departmentDTO.Deparment = alphabet[j - 1].ToString();
                                departmentDTOs.Add(departmentDTO);
                            }
                            else
                            {
                                departmentDTO.Floor = (floorIndex).ToString();
                                departmentDTO.Deparment = alphabet[j - 1].ToString();
                                departmentDTOs.Add(departmentDTO);
                            }
     
                        }
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

                    if (logicGenrationDTO.HasLowLevel)
                    {
                        for (int j = 1; j <= countDeparmentsUniform; j++)
                        {
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                            floorDTO.Floor = "PB"; 
                            floorDTO.Deparment = j.ToString();
                            floorDTOs.Add(floorDTO);
                        }
                    }


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
                    for (int i = 0; i <= logicGenrationDTO.Floors; i++)
                    {
                        if (i == 0 && logicGenrationDTO.HasLowLevel)
                        {
                            i = 1;
                            for (int j = 1; j <= countDeparmentsUniform; j++)
                            {
                                FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                                floorDTO.Floor = "PB";
                                floorDTO.Deparment = (i * logicGenrationDTO.Iteration.Value + j).ToString();
                                floorDTOs.Add(floorDTO);
                            }                           
                        }                       

                        for (int j = 1; j <= countDeparmentsUniform; j++)
                        {   
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                            if (logicGenrationDTO.HasLowLevel)
                            {         
                                floorDTO.Floor = i.ToString();
                                int departmentNumber = (i + 1) * logicGenrationDTO.Iteration.Value + j;
                                floorDTO.Deparment = departmentNumber.ToString();
                                floorDTOs.Add(floorDTO);
                            }
                            else
                            {
                                i = i == 0 ? 1 : i;
                                floorDTO.Floor = i.ToString();
                                int departmentNumber = i * logicGenrationDTO.Iteration.Value + j;
                                floorDTO.Deparment = departmentNumber.ToString();
                                floorDTOs.Add(floorDTO);
                            }

                        }
                    }
                }
            }


            if (departmentsPerFloor.Count > 0)
            {
                if (logicGenrationDTO.Sequential)
                {
                    int floorIndex = 0;
                    int departmentIndex = 1;

                    foreach (int departmentsCount in departmentsPerFloor)
                    {
                        for (int i = 0; i < departmentsCount; i++)
                        {
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();

                            if(floorIndex == 0 && logicGenrationDTO.HasLowLevel)
                            {
                                floorDTO.Floor = "PB";
                                floorDTO.Deparment = departmentIndex.ToString();
                                floorDTOs.Add(floorDTO);
                                departmentIndex++;
                            }
                            else
                            {
                                floorDTO.Floor = (floorIndex + 1).ToString();
                                floorDTO.Deparment = departmentIndex.ToString();
                                floorDTOs.Add(floorDTO);
                                departmentIndex++;
                            }

                        }

                        floorIndex++;
                    }
                }
                else
                {
                    int floorIndex = 0;
                    foreach (int departmentsCount in departmentsPerFloor)
                    {
                        if(floorIndex == 0 && logicGenrationDTO.HasLowLevel)
                        {
                            floorIndex = 1;
                            for (int j = 1; j <= departmentsCount; j++)
                            {
                                FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();
                                floorDTO.Floor = "PB";
                                int departmentNumber = floorIndex * logicGenrationDTO.Iteration.Value + j;
                                floorDTO.Deparment = departmentNumber.ToString();
                                floorDTOs.Add(floorDTO);
                            }
                        }

                        for (int j = 1; j <= departmentsCount; j++)
                        {
                            FloorDepartmentDTO floorDTO = new FloorDepartmentDTO();

                            if (logicGenrationDTO.HasLowLevel)
                            {
                                floorDTO.Floor = floorIndex.ToString();
                                int departmentNumber = (floorIndex + 1) * logicGenrationDTO.Iteration.Value + j;
                                floorDTO.Deparment = departmentNumber.ToString();
                                floorDTOs.Add(floorDTO);
                            }
                            else
                            {
                                floorIndex = floorIndex == 0 ? 1 : floorIndex;
                                floorDTO.Floor = floorIndex.ToString();
                                int departmentNumber = floorIndex * logicGenrationDTO.Iteration.Value + j;
                                floorDTO.Deparment = departmentNumber.ToString();
                                floorDTOs.Add(floorDTO);
                            }

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
