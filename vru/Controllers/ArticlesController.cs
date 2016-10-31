using SX.WebCore.MvcControllers;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using SX.WebCore.Repositories;

namespace vru.Controllers
{
    [AllowAnonymous]
    public sealed class ArticlesController : SxMaterialsController<Article, VMArticle>
    {
        private static RepoArticles _repo = new RepoArticles();
        public ArticlesController() : base(ModelCoreTypeProvider[nameof(Article)]) { }
        public override SxRepoMaterial<Article, VMArticle> Repo
        {
            get
            {
                return _repo;
            }
        }

        [HttpGet]
        public override ActionResult List(int page = 1, int pageSize = 10)
        {
            return base.List(page, 2);
        }

        [HttpGet]
        public ActionResult Details(int year, string month, string day, string titleUrl)
        {
            var viewModel = Repo.GetByTitleUrl(year, month, day, titleUrl);
            if (viewModel == null)
                return new HttpNotFoundResult();

            return View(viewModel);
        }
    }
}