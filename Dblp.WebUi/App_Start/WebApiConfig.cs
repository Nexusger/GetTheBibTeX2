using System.Net;
using System.Web.Http;

namespace Dblp.WebUi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste
          
            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}/{action}/{*key}",
                defaults: new {key = RouteParameter.Optional}
            );


        }
    }
}
