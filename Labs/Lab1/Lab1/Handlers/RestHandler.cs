using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Lab1.Handlers
{
    public class RestHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Session["Stack"] == null)
                {
                    context.Session["Stack"] = new Stack<int>();
                }

                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        ProcessGet(context);
                        break;
                    case "POST":
                        ProcessPost(context);
                        break;
                    case "PUT":
                        ProcessPut(context);
                        break;
                    case "DELETE":
                        ProcessDelete(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var jsonSerializer = new JavaScriptSerializer();

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonSerializer.Serialize(new
                {
                    StatusCode = 500,
                    Message = ex.Message,
                }));
            }
        }

        private static void ProcessGet(HttpContext context)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var stack = (Stack<int>)context.Session["Stack"];
            var peek = stack.Count > 0 ? stack.Peek() : 0;

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonSerializer.Serialize(new
            {
                Result = GlobalVariables.Result + peek,
            }));
        }

        private static void ProcessPost(HttpContext context)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var stack = (Stack<int>)context.Session["Stack"];
            var peek = stack.Count > 0 ? stack.Peek() : 0;

            int.TryParse(context.Request.Form["Result"], out var newResult);
            GlobalVariables.Result = newResult;

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonSerializer.Serialize(new
            {
                Result = GlobalVariables.Result + peek,
            }));
        }

        private static void ProcessPut(HttpContext context)
        {
            int.TryParse(context.Request.Form["add"], out var add);

            var stack = (Stack<int>)context.Session["Stack"];
            stack.Push(add);
            context.Session["Stack"] = stack;
        }

        private static void ProcessDelete(HttpContext context)
        {
            var stack = (Stack<int>)context.Session["Stack"];
            if (stack.Count == 0)
            {
                throw new InvalidOperationException("Can't perform stack pop, because it is empty");
            }

            stack.Pop();
        }

        #endregion
    }
}
