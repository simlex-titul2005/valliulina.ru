using SX.WebCore.MvcControllers.Abstract;
using SX.WebCore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace vru.Infrastructure
{
    public static class BreadcrumbsManager
    {
        public static void WriteBreadcrumbs(this SxBaseController controller, HashSet<SxVMBreadcrumb> breadcrumbs)
        {
            if (controller.ControllerContext.IsChildAction) return;

            breadcrumbs.Add(new SxVMBreadcrumb { Title = "Главная", Url = "/" });

            controller.ViewBag.Breadcrumbs = breadcrumbs.ToArray();
        }
    }
}