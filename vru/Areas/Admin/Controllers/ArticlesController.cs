using SX.WebCore;
using SX.WebCore.MvcControllers;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.Enums;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Areas.Admin.Controllers
{
    public sealed class ArticlesController : SxMaterialsController<Article, VMArticle, DbContext>
    {
        public ArticlesController() : base(ModelCoreType.Article)
        {
            if (Repo == null)
                Repo = new RepoArticles();
        }

        private readonly int _pageSize = 10;

        [HttpPost]
        public PartialViewResult Index(VMArticle filterModel, SxOrder order, int page = 1)
        {
            var fct=Request.Form["filterModel[FilterCategoryTitle]"];
            var filter = new SxFilter(page, _pageSize) {
                Order = order != null && order.Direction != SortDirection.Unknown ? order : null,
                WhereExpressionObject = filterModel,
                AddintionalInfo=new object[] { fct }
            };

            var viewModel = Repo.Read(filter);
            filter.PagerInfo.Page = filter.PagerInfo.TotalItems <= _pageSize ? 1 : page;

            ViewBag.Filter = filter;

            return PartialView("_GridView", viewModel);
        }
    }
}