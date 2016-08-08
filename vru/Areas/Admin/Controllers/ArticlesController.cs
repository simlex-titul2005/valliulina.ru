using Microsoft.AspNet.Identity;
using SX.WebCore;
using SX.WebCore.MvcControllers;
using System;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.Enums;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Areas.Admin.Controllers
{
    public sealed class ArticlesController : SxMaterialsController<Article, DbContext>
    {
        private static RepoArticles _repo;
        public ArticlesController() : base(ModelCoreType.Article)
        {
            if (_repo == null)
                _repo = new RepoArticles();
        }

        private readonly int _pageSize = 10;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var order = new SxOrder { FieldName = "dm.DateCreate", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order };
            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            var viewModel = data
                .Select(x => Mapper.Map<Article, VMArticle>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult Index(VMArticle filterModel, SxOrder order, int page = 1)
        {
            var fct=Request.Form["filterModel[FilterCategoryTitle]"];
            var filter = new SxFilter(page, _pageSize) {
                Order = order != null && order.Direction != SortDirection.Unknown ? order : null,
                WhereExpressionObject = filterModel,
                AddintionalInfo=new object[] { fct }
            };

            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            filter.PagerInfo.Page = filter.PagerInfo.TotalItems <= _pageSize ? 1 : page;

            var viewModel = data
                .Select(x => Mapper.Map<Article, VMArticle>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return PartialView("_GridView", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var data = id.HasValue ? _repo.GetByKey(id, ModelCoreType.Article) : new Article { ModelCoreType=ModelCoreType.Article};
            if (id.HasValue && data == null)
                return new HttpNotFoundResult();

            if (!id.HasValue)
                data.DateOfPublication = DateTime.Now.AddHours(1);

            if (data.FrontPicture != null)
                ViewData["FrontPictureIdCaption"] = data.FrontPicture.Caption;

            if (data.Category != null)
                ViewBag.MaterialCategoryTitle = data.Category.Title;

            ViewBag.ModelCoreType = ModelCoreType.Article;

            var viewModel = Mapper.Map<Article, VMArticle>(data);
            return View(viewModel);
        }

        [HttpPost]
        public  ActionResult Edit(VMArticle model)
        {
            var isNew = model.Id == 0;
            if (isNew || (!isNew && string.IsNullOrEmpty(model.TitleUrl)))
            {
                model.TitleUrl = Url.SeoFriendlyUrl(model.Title);
                ModelState["TitleUrl"].Errors.Clear();
            }

            if(ModelState.IsValid)
            {
                model.UserId = User.Identity.GetUserId();

                var redactModel = Mapper.Map<VMArticle, Article>(model);
                if (isNew)
                {
                    _repo.Create(redactModel);
                }
                else
                    _repo.Update(redactModel, true, "Title", "Show", "DateOfPublication", "CategoryId", "FrontPictureId", "ShowFrontPictureOnDetailPage", "TitleUrl", "Html");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ModelCoreType = ModelCoreType.Article;
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public  ActionResult Delete(Article model)
        {
            var data = _repo.GetByKey(model.Id, model.ModelCoreType);
            if (data == null)
                return new HttpNotFoundResult();

            _repo.Delete(model);
            return RedirectToAction("Index");
        }
    }
}