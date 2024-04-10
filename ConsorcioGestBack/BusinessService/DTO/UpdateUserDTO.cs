using BusinessService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.DTO
{
    public class UpdateUserDTO
    {
        public string? Phone { get; set; } = null;
        public string? Email { get; set; } = null;
        public int? IdProfile { get; set; }
        public int? IdUserState { get; set; }
        public int? IdCondominium { get; set; }
    }
}
