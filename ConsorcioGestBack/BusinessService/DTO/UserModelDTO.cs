﻿using BusinessService.Enums;
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
        public string Profile { get; set; }
        public string Property { get; set; }
        public string State { get; set; }
     }
}
