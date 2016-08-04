using System.Web.Mvc;

namespace vru.Controllers
{
    public sealed class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }
    }
}