using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace BusinessService.DTO
{
    public class SaveClaimDTO
    {
        public int CauseClaimID { get; set; }
        public int AffectedSpaceID { get; set; }
        public string ProblemDetail { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    public class ImagesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DatosImagen { get; set; }
        public int IdReclamo { get; set; }
    }

    public class SaveClaimGestionDTO 
    {
        public int IdClaim { get; set; }
        public string Observation {  get; set; }
        public int StateSelectedID { get; set; }
    }

}
