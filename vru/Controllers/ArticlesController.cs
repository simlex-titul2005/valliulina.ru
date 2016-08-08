using SX.WebCore;
using SX.WebCore.MvcControllers;
using SX.WebCore.Repositories;
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
    public sealed class ArticlesController : SxMaterialsController<Article, DbContext>
    {
        private static RepoArticles _repo;
        private static SxRepoSeoTags<DbContext> _repoSeoTags;
        public ArticlesController() : base(Enums.ModelCoreType.Article)
        {
            if (_repo == null)
                _repo = new RepoArticles();
            if (_repoSeoTags == null)
                _repoSeoTags = new SxRepoSeoTags<DbContext>();
        }

        private readonly int _pageSize = 2;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var cat = Request.QueryString.Get("c");

            var order = new SxOrder { FieldName = "dm.DateOfPublication", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order, WhereExpressionObject=cat==null?null:new VMArticle { CategoryId=cat } };
            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);

            if (page > 1 && !data.Any())
                return new HttpNotFoundResult();

            filter.PagerInfo.TotalItems = totalItems;
            filter.PagerInfo.PagerSize = 3;
            var viewData = data
                .Select(x => Mapper.Map<Article, VMArticle>(x))
                .ToArray();
            var viewModel = new SxPagedCollection<VMArticle>
            {
                Collection = viewData,
                PagerInfo = filter.PagerInfo
            };

            ViewBag.CategoryId = cat;
            ViewBag.Filter = filter;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int year, string month, string day, string titleUrl)
        {
            var data = _repo.GetByTitleUrl(year, month, day, titleUrl);
            if (data == null)
                return new HttpNotFoundResult();

            var viewModel = Mapper.Map<Article, VMArticle>(data);
            return View(viewModel);
        }
    }
}