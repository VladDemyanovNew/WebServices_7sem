using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;
using System.Collections;

using Ploeh.Hyprlinkr;

using Lab3.Core.Exceptions;
using Lab3.WebApi.Constants;
using Lab3.WebApi.Models;
using Lab3.WebApi.Models.Resources;

namespace Lab3.WebApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var isErrorsObjNotExist = HttpContext.Current.Session["Errors"] == null;
            if (isErrorsObjNotExist)
            {
                HttpContext.Current.Session["Errors"] = new Hashtable();
            }

            var jsonSerializer = new JavaScriptSerializer();

            if (actionExecutedContext.Exception is ApiEntityNotFoundException)
            {
                var guid = Guid.NewGuid().ToString();
                this.SaveLastError(new ErrorResponse
                {
                    ErrorMessage = actionExecutedContext.Exception.Message,
                    StatusCode = (int)HttpStatusCode.NotFound,
                }, guid);

                var linker = new RouteLinker(actionExecutedContext.Request);
                var responseBody = new ErrorResource
                {
                    Links = ResourcesLinks.GetErrorResourceLinks(guid, linker),
                };

                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(jsonSerializer.Serialize(responseBody), Encoding.UTF8, "application/json"),
                };
            }
        }

        private void SaveLastError(ErrorResponse errorResponse, string guid)
        {
            var errors = (Hashtable)HttpContext.Current.Session["Errors"];
            errors.Add(guid, errorResponse);

            HttpContext.Current.Session["Errors"] = errors;
        }
    }
}