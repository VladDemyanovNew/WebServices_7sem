using System.Web;
using System.Web.Http;

using Lab3.WebApi.Formatters;


namespace Lab3.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public static void ConfigureApis(HttpConfiguration config)
        {
            // config.Formatters.Add(new StudentResourceFormatter());
        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
