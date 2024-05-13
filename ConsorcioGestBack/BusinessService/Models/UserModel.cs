using BusinessService.Models.AuxModel;
using DataAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public bool IsOwner { get; set; }
        public bool IsOcupant { get; set; }
        public int? IdCondominium { get; set; }
        public string Condominio { get; set; }
        public ProfileModel Profile { get; set; } = null;
        public StateModel UserState { get; set; } = null;
        public int? IdDocumentType { get; set; }
        public int Document { get; set; }
        public string Token { get; set; }
    }
}
