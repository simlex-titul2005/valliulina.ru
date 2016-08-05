using SX.WebCore.MvcControllers;
using System.Web.Mvc;
using vru.Infrastructure;

namespace vru.Controllers
{
    public abstract class BaseController : SxBaseController<DbContext>
    {
        public BaseController()
        {
            WriteBreadcrumbs = BreadcrumbsManager.WriteBreadcrumbs;
        }
    }
}