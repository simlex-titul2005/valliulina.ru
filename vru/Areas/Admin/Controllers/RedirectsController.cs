using SX.WebCore.MvcControllers;
using System.Web.Mvc;
using vru.Infrastructure;

namespace vru.Areas.Admin.Controllers
{
    [Authorize(Roles = "seo")]
    public sealed class RedirectsController : Sx301RedirectsController<DbContext>
    {

    }
}