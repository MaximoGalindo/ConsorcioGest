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

    public class ImageDTO
    {
        public IFormFile File { get; set; }
    }
}
