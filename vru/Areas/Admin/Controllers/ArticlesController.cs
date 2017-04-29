using System;
using SX.WebCore.MvcControllers;
using SX.WebCore.SxRepositories;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;

namespace vru.Areas.Admin.Controllers
{
    public sealed class ArticlesController : SxMaterialsController<Article, VMArticle>
    {
        public override SxRepoMaterial<Article, VMArticle> Repo => new RepoArticles();
        public ArticlesController() : base(ModelCoreTypeProvider[nameof(Article)]) { }
    }
}