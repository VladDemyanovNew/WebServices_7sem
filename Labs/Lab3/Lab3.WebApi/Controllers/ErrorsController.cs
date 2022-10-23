using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using Lab3.WebApi.Models;

namespace Lab3.WebApi.Controllers
{
    public class ErrorsController : ApiController
    {
        public ErrorResponse Get(string guid)
        {
            var errors = (Hashtable)HttpContext.Current.Session["Errors"];

            return (ErrorResponse)errors[guid];
        }
    }
}
