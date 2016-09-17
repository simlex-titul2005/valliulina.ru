using SX.WebCore.MvcControllers.Abstract;
using System.Web.Mvc;

namespace vru.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class BaseController : SxBaseController
    {
        
    }
}