using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class EmailResponseModel<T> where T : class
    {
        public StatusResponse Status { get; set; }
        public string Message { get; set; } 
    }
}
