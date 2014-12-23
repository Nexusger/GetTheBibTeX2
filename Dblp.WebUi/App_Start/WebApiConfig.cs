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
                name: "PrefetchApi",
                routeTemplate: "api/Prefetch/{id}",
                defaults: new { controller="Prefetch"}
            );
            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: "api/{controller}/{query}"
            );


        }
    }
}
