using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                    name: "TheUsers",
                    routeTemplate: "api/users/{id}",
                    defaults: new { controller = "Values", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                    name: "PostUsers",
                    routeTemplate: "api/users",
                    defaults: new
                    {
                        controller = "Values",
                        httpMethod = new HttpMethodConstraint("POST")
                    }
               );

            config.Routes.MapHttpRoute(
                    name: "PostUsersSetPoints",
                    routeTemplate: "api/setPoints/",
                    defaults: new
                    {
                        controller = "Points",
                        action = "SetPoints"
                    }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
