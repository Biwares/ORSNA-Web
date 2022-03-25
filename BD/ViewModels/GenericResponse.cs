using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.ViewModels
{
    public class GenericResponse<T>
    {
        public int Code
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }

        public T Result
        {
            get;
            set;
        }

    }
}