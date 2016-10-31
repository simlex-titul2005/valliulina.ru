using System.Web.Mvc;

namespace vru.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: null,
                url: "Admin",
                defaults: new { controller = "Home", action = "Index", area = "Admin" }
            );

            context.MapRoute(
                name: null,
                url: "Admin/MaterialCategories/Edit/{mct}/{id}",
                defaults: new { controller = "MaterialCategories", action = "Edit", id = UrlParameter.Optional, area = "Admin" }
            );

            context.MapRoute(
                name: null,
                url: "Admin/MaterialCategories/Delete/{mct}/{id}",
                defaults: new { controller = "MaterialCategories", action = "Delete", id = UrlParameter.Optional, area = "Admin" }
            );

            context.MapRoute(
                name: null,
                url: "Admin/MaterialCategories/FindTreeView/{mct}",
                defaults: new { controller = "MaterialCategories", action = "FindTreeView", area = "Admin" }
            );

            context.MapRoute(
                name: null,
                url: "Admin/MaterialCategories/{mct}",
                defaults: new { controller = "MaterialCategories", action = "Index", area = "Admin" }
            );

            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, area = "Admin" }
            );
        }
    }
}