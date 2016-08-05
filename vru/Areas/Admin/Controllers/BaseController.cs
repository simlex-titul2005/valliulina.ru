using SX.WebCore.MvcControllers;
using System.Web.Mvc;

namespace vru.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class BaseController : SxBaseController<Infrastructure.DbContext>
    {
        
    }
}