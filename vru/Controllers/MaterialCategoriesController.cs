using SX.WebCore.DbModels;
using SX.WebCore.MvcControllers;
using SX.WebCore.ViewModels;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.ViewModels;

namespace vru.Controllers
{
    public sealed class MaterialCategoriesController : SxMaterialCategoriesController<SxMaterialCategory,SxVMMaterialCategory>
    {
        public MaterialCategoriesController()
        {
            if (Repo == null || !(Repo is RepoMaterialCategory))
                Repo = new RepoMaterialCategory();
        }

        [ChildActionOnly, AllowAnonymous]
        public PartialViewResult List(string current = null)
        {
            var viewModel = (Repo as RepoMaterialCategory).All().Select(x => new VMCategory
            {
                Title = x.Title,
                Url = Url.Action("Index", "Articles", new { category = x.Id }),
                IsCurrent=current==x.Id?.ToString()
            }).ToArray();

            return PartialView("_List", viewModel);
        }
    }
}