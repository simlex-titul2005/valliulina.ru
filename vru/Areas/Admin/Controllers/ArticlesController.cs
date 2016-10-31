using SX.WebCore.MvcControllers;
using SX.WebCore.Repositories;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;

namespace vru.Areas.Admin.Controllers
{
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
    }
}