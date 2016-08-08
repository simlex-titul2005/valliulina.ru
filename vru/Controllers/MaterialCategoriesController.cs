using SX.WebCore;
using SX.WebCore.MvcControllers;
using SX.WebCore.Repositories;
using SX.WebCore.ViewModels;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.ViewModels;

namespace vru.Controllers
{
    public sealed class MaterialCategoriesController : SxMaterialCategoriesController<DbContext>
    {
        private static SxRepoMaterialCategory<DbContext> _repo;
        public MaterialCategoriesController()
        {
            if (_repo == null)
                _repo = new SxRepoMaterialCategory<DbContext>();
        }

        [ChildActionOnly, AllowAnonymous]
        public PartialViewResult List(string current = null)
        {
            var data = _repo.All.Where(x => x.ParentCategoryId == null).ToArray();

            var viewModel = data.Select(x => Mapper.Map<SxMaterialCategory, SxVMMaterialCategory>(x)).Select(x => new VMCategory
            {
                Title = x.Title,
                Url = Url.Action("Index", "Articles", new { c = x.Id }),
                IsCurrent=current==x.Id
            }).ToArray();

            return PartialView("_List", viewModel);
        }
    }
}