using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Lab3.WebApi.Filters
{
    public class ResponseContentTypeAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                bool isXml = actionContext.Request.RequestUri.Segments[2].EndsWith(".xml/");
                if (isXml)
                {
                    actionContext.Request.Headers.Accept.Clear();
                    actionContext.Request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/xml"));
                }
            });
        }
    }
}