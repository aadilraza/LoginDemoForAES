using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace olympic
{
    public static class WebApiConfig
    {

        //  [EnableCors("*", "*", "*")]
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "api/AESAlgorithum",
            //    routeTemplate: "api/AESAlgorithum",
            //    defaults: new
            //    {
            //        controller = "AES",
            //        action = "AESAlgorithum"
            //    }
            //);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

        }

    }
}



