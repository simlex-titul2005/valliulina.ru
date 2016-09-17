using SX.WebCore.MvcControllers;
using System.Web.Mvc;

namespace vru.Areas.Admin.Controllers
{
    [Authorize(Roles = "seo")]
    public sealed class RedirectsController : SxRedirectsController
    {

    }
}