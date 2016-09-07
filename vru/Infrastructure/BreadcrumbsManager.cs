using SX.WebCore.MvcControllers;
using SX.WebCore.ViewModels;
using System.Collections.Generic;

namespace vru.Infrastructure
{
    public static class BreadcrumbsManager
    {
        public static void WriteBreadcrumbs(SxBaseController<DbContext> controller)
        {
            if (controller.ControllerContext.IsChildAction) return;

            var routes = controller.ControllerContext.RequestContext.RouteData.Values;
            var gameName = routes["game"] != null && !string.IsNullOrEmpty(routes["game"].ToString()) ? routes["game"].ToString() : null;

            var breadcrumbs = new List<SxVMBreadcrumb>();
            breadcrumbs.Add(new SxVMBreadcrumb { Title = "Главная", Url = "/" });

            controller.ViewBag.Breadcrumbs = breadcrumbs.ToArray();
        }
    }
}