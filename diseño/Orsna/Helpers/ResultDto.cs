using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orsna.Helpers
{
    public class ResultDto<T> where T : class
    {
        public ResultDto(string status, ICollection<T> data)
        {
            Status = status;
            Data = data;
        }
        public string Status { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
