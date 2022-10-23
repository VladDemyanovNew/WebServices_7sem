using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.WebApi.Models
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }

        public int StatusCode { get; set; }
    }
}