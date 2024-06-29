using BusinessService.Enums;
using BusinessService.Models.AuxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class UserModelDTO
    {
        public int Document { get; set; }
        public string Name { get; set; }
        public string Condominium { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ProfileModel Profile { get; set; }
        public string Property { get; set; }
        public StateModel State { get; set; }
    }

    public class UserModelByDocumentDTO 
    {
        public int Document { get; set; }
        public string Name { get; set; }
        public string Tower { get; set; }
        public string Condominium { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ProfileModel Profile { get; set; }
        public string Property { get; set; }
        public StateModel State { get; set; }
    }

}
