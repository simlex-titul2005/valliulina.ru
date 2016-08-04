using System.Web.Mvc;

namespace vru.Controllers
{
    public sealed class ArticlesController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}