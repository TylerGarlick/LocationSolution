using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LocationSolution.Mvc.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "Cities",
               routeTemplate: "api/states/{stateId}/cities/{id}",
               defaults: new { id = RouteParameter.Optional, controller = "Cities" }
           );

            config.Routes.MapHttpRoute(
                name: "StatesApi",
                routeTemplate: "api/countries/{countryCode}/states/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "States" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
