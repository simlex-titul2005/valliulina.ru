using System.Web.Mvc;
using System.Web.Routing;

namespace vru
{
    public class RouteConfig
    {
        private static readonly string[] _defNamespases = new string[] { "vru.Controllers" };

        public static void PreRouteAction(RouteCollection routes)
        {
            #region articles
            routes.MapRoute(
                    name: null,
                    url: "Articles",
                    defaults: new { controller = "Articles", action = "List", page = 1, area = "" },
                    namespaces: _defNamespases
                );

            routes.MapRoute(
                name: null,
                url: "Articles/page{page}",
                defaults: new { controller = "Articles", action = "List", page = 1, area = "" },
                namespaces: _defNamespases
            );
            #endregion
        }

        public static void PostRouteAction(RouteCollection route)
        {

        }
    }
}
