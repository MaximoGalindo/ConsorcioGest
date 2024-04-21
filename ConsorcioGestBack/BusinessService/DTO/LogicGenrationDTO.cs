using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class LogicGenrationDTO
    {
        public int? Floors {  get; set; }
        public List<CountDeparmentsByFloor>? CountDepartmentsByFloor { get; set; }
        public List<char>? Alphabet { get; set; }
        public int? Iteration { get; set; }
        public bool Sequential { get; set; }
    }
}
