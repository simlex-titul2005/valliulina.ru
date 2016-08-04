using System.Web.Mvc;
using System.Web.Routing;

namespace vru
{
    public class RouteConfig
    {
        private static readonly string[] _defNamespases = new string[] { "vru.Controllers" };

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            

            routes.MapRoute(
                name: null,
                url: "{controller}/Details/{titleUrl}",
                defaults: new { controller = "Services", action = "Details", area = "" },
                namespaces: _defNamespases
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, area="" },
                namespaces:_defNamespases
            );
        }
    }
}
