using SX.WebCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Areas.Admin.Controllers
{
    public sealed class EducationController : BaseController
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
            var order = new SxOrder { FieldName = "de.[Order]", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order };

            var viewModel = _repo.Read(filter);
            if (page > 1 && !viewModel.Any())
                return new HttpNotFoundResult();

            ViewBag.Filter = filter;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(VMEducation filterModel, SxOrder order, int page = 1)
        {
            var filter = new SxFilter(page, _pageSize) { Order = order != null && order.Direction != SortDirection.Unknown ? order : null, WhereExpressionObject = filterModel };

            var viewModel = await _repo.ReadAsync(filter);
            if (page > 1 && !viewModel.Any())
                return new HttpNotFoundResult();

            ViewBag.Filter = filter;

            return PartialView("_GridView", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var data = id.HasValue ? _repo.GetByKey(id) : new Education();
            if (data == null)
                return new HttpNotFoundResult();

            if (id.HasValue && data.Picture != null)
                ViewData["PictureIdCaption"] = data.Picture.Caption;

            var viewModel = Mapper.Map<Education, VMEducation>(data);

            var years = new List<SelectListItem>();
            for (int i = 1950; i <= DateTime.Now.Year; i++)
            {
                years.Add(new SelectListItem { Text=i.ToString(), Value=i.ToString(), Selected= id.HasValue && data.Year==i});
            }
            ViewBag.Years = years.ToArray();

            var monthes = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                var monthName= CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                monthes.Add(new SelectListItem { Text=monthName, Value=i.ToString(), Selected= id.HasValue && data.Month == i });
            }
            ViewBag.Monthes = monthes.ToArray();

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

        [HttpPost]
        public ActionResult Delete(Education model)
        {
            var data = _repo.GetByKey(model.Id);
            if (data == null)
                return new HttpNotFoundResult();

            _repo.Delete(model);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult ChangeOrder(int id, bool dir, int page=1)
        //{
        //    _repo.ChangeOrder(id, dir);

        //    var order = new SxOrder { FieldName = "de.[Order]", Direction = SortDirection.Desc };
        //    var filter = new SxFilter(page, _pageSize) { Order = order };
        //    var totalItems = 0;
        //    var data = _repo.Read(filter, out totalItems);
        //    filter.PagerInfo.TotalItems = totalItems;
        //    var viewModel = data
        //        .Select(x => Mapper.Map<Education, VMEducation>(x))
        //        .ToArray();

        //    ViewBag.Filter = filter;

        //    return PartialView("_GridView", viewModel);
        //}
    }
}