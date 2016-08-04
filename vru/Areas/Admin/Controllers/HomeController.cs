using System.Web.Mvc;

namespace vru.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}