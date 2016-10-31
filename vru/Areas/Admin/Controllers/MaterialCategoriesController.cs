using System.Web.Mvc;
using SX.WebCore.DbModels;
using SX.WebCore.MvcControllers;
using SX.WebCore.ViewModels;

namespace vru.Areas.Admin.Controllers
{
    public sealed class MaterialCategoriesController : SxMaterialCategoriesController<SxMaterialCategory, SxVMMaterialCategory>
    {
        public override ActionResult Index(byte? mct, int page = 1)
        {
            ViewBag.PageTitle = "Категории статей";
            return base.Index(mct, page);
        }
    }
}