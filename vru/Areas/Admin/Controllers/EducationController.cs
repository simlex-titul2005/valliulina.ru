using SX.WebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Areas.Admin.Controllers
{
    public class EducationController : BaseController
    {
        private static RepoEducation _repo;
        public EducationController()
        {
            if (_repo == null)
                _repo = new RepoEducation();
        }

        private static readonly int _pageSize = 10;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var order = new SxOrder { FieldName = "DateCreate", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order };
            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            var viewModel = data
                .Select(x => Mapper.Map<Education, VMEducation>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return View(viewModel);
        }

        [HttpPost]
        public virtual PartialViewResult Index(VMEducation filterModel, SxOrder order, int page = 1)
        {
            var filter = new SxFilter(page, _pageSize) { Order = order != null && order.Direction != SortDirection.Unknown ? order : null, WhereExpressionObject = filterModel };

            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            filter.PagerInfo.Page = filter.PagerInfo.TotalItems <= _pageSize ? 1 : page;

            var viewModel = data
                .Select(x => Mapper.Map<Education, VMEducation>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return PartialView("_GridView", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var data = id.HasValue ? _repo.GetByKey(id) : new Education();
            if (data == null)
                return new HttpNotFoundResult();

            var viewModel = Mapper.Map<Education, VMEducation>(data);

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(VMEducation model)
        {
            var isNew = model.Id == 0;

            if (ModelState.IsValid)
            {
                var redactModel = Mapper.Map<VMEducation, Education>(model);
                if (isNew)
                    _repo.Create(redactModel);
                else
                    _repo.Update(redactModel);

                return RedirectToAction("Index");
            }

            else return View(model);
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Delete(Education model)
        {
            var data = _repo.GetByKey(model.Id);
            if (data == null)
                return new HttpNotFoundResult();

            _repo.Delete(model);
            return RedirectToAction("Index");
        }
    }
}