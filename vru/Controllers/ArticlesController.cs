using SX.WebCore;
using SX.WebCore.MvcControllers;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Controllers
{
    [AllowAnonymous]
    public sealed class ArticlesController : SxMaterialsController<Article, VMArticle, DbContext>
    {
        public ArticlesController() : base(Enums.ModelCoreType.Article)
        {
            if (Repo == null || !(Repo is RepoArticles))
                Repo = new RepoArticles();
        }

        [HttpGet]
        public ActionResult Details(int year, string month, string day, string titleUrl)
        {
            var viewModel = Repo.GetByTitleUrl(year, month, day, titleUrl);
            if (viewModel == null)
                return new HttpNotFoundResult();

            return View(viewModel);
        }

        private static int _pageSize = 2;
        [HttpGet]
        public override ActionResult Index(int page = 1)
        {
            var cat = Request.QueryString.Get("c");
            if (!string.IsNullOrEmpty(cat))
                ViewBag.CategoryId = cat;

            var order = new SxOrder { FieldName = "dm.DateOfPublication", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order, WhereExpressionObject = cat == null ? null : new VMArticle { CategoryId = cat } };

            var data = Repo.Read(filter);
            if (page > 1 && !data.Any())
                return new HttpNotFoundResult();

            var viewModel = new SxPagedCollection<VMArticle>()
            {
                Collection = data,
                PagerInfo = filter.PagerInfo
            };

            ViewBag.Filter = filter;

            return View(viewModel);
        }
    }
}