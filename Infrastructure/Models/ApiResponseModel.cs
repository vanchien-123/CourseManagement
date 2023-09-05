using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Models
{
    public class ApiResponseModel<T> where T : class
    {
        public T Data { get; set; }
        public StatusResponse Status { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
