using SX.WebCore.MvcControllers.Abstract;
using System.Web.Mvc;
using vru.Infrastructure;

namespace vru.Controllers
{
    [AllowAnonymous]
    public abstract class BaseController : SxBaseController
    {
        public BaseController()
        {
            WriteBreadcrumbs = BreadcrumbsManager.WriteBreadcrumbs;
        }
    }
}