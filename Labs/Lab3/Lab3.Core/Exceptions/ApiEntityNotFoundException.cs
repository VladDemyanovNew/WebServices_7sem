using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Core.Exceptions
{
    public class ApiEntityNotFoundException : Exception
    {
        public ApiEntityNotFoundException()
        {
        }

        public ApiEntityNotFoundException(string message) : base(message)
        {
        }

        public ApiEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
