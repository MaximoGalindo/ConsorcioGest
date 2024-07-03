using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.ResponseDTO
{
    public class SuccessResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
