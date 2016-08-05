﻿using SX.WebCore;
using SX.WebCore.MvcControllers;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public sealed class SituationsController : BaseController
    {
        private static RepoSituations _repo;
        public SituationsController()
        {
            if (_repo == null)
                _repo = new RepoSituations();
        }

        private static readonly int _pageSize = 10;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var order = new SxOrder { FieldName = "ds.[Text]", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order };
            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            var viewModel = data
                .Select(x => Mapper.Map<Situation, VMSituation>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult Index(VMSituation filterModel, SxOrder order, int page = 1)
        {
            var filter = new SxFilter(page, _pageSize) { Order = order != null && order.Direction != SortDirection.Unknown ? order : null, WhereExpressionObject = filterModel };

            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            filter.PagerInfo.Page = filter.PagerInfo.TotalItems <= _pageSize ? 1 : page;

            var viewModel = data
                .Select(x => Mapper.Map<Situation, VMSituation>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return PartialView("_GridView", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var data = id.HasValue ? _repo.GetByKey(id) : new Situation();
            if (data == null)
                return new HttpNotFoundResult();

            var viewModel = Mapper.Map<Situation, VMSituation>(data);

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(VMSituation model)
        {
            var isNew = model.Id == 0;

            if (ModelState.IsValid)
            {
                var redactModel = Mapper.Map<VMSituation, Situation>(model);
                if (isNew)
                    _repo.Create(redactModel);
                else
                    _repo.Update(redactModel);

                return RedirectToAction("Index");
            }

            else return View(model);
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Delete(Situation model)
        {
            var data = _repo.GetByKey(model.Id);
            if (data == null)
                return new HttpNotFoundResult();

            _repo.Delete(model);
            return RedirectToAction("Index");
        }
    }
}