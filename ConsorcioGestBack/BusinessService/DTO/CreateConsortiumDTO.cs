using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class CreateConsortiumDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string CUIT { get; set; }
        public ConsortiumConfiguration Configuration { get; set; }
    }
}
