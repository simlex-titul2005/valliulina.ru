using SX.WebCore.MvcControllers.Abstract;
using SX.WebCore.ViewModels;
using System.Collections.Generic;

namespace vru.Infrastructure
{
    public static class BreadcrumbsManager
    {
        public static void WriteBreadcrumbs(this SxBaseController controller)
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