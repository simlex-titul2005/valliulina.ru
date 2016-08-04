using System.Web.Mvc;

namespace vru.Controllers
{
    public sealed class EducationController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}