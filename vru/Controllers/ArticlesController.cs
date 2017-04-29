using SX.WebCore.MvcControllers;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using SX.WebCore.SxRepositories;
using System.Threading.Tasks;

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
        public override Task<ActionResult> List(int page = 1, int pageSize = 2, bool? withPictures = default(bool?))
        {
            return base.List(page, pageSize, withPictures);
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