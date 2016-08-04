using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static vru.Infrastructure.HtmlHelpers.Extantions;
using vru.Infrastructure.Extantions;

namespace vru.Areas.Admin.Controllers
{
    public class ServicesController : BaseController
    {
        private static RepoServices _repo;
        public ServicesController()
        {
            if (_repo == null)
                _repo = new RepoServices();
        }

        private static readonly int _pageSize=3;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var order = new Order { FieldName = "DateCreate", Direction = SortDirection.Desc };
            var filter = new Infrastructure.Filter(page, _pageSize) { Order = order };
            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            var viewModel = data
                .Select(x => Mapper.Map<Service, VMService>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return View(viewModel); 
        }

        [HttpPost]
        public virtual PartialViewResult Index(VMService filterModel, Order order, int page = 1)
        {
            var filter = new Infrastructure.Filter(page, _pageSize) { Order = order != null && order.Direction != SortDirection.Unknown ? order : null, WhereExpressionObject = filterModel };

            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            filter.PagerInfo.TotalItems = totalItems;
            filter.PagerInfo.Page = filter.PagerInfo.TotalItems <= _pageSize ? 1 : page;

            var viewModel = data
                .Select(x => Mapper.Map<Service, VMService>(x))
                .ToArray();

            ViewBag.Filter = filter;

            return PartialView("_GridView", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var data = id.HasValue ? _repo.GetById(new object[] { id }) : new Service();
            if (data == null)
                return new HttpNotFoundResult();

            var viewModel = Mapper.Map<Service, VMService>(data);

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(VMService model)
        {
            var isNew = model.Id == 0;
            if(isNew || (!isNew && model.TitleUrl==null))
            {
                model.TitleUrl = Url.SeoFriendlyUrl(model.Title);
                ModelState["TitleUrl"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                var redactModel = Mapper.Map<VMService, Service>(model);
                if (isNew)
                    _repo.Create(redactModel);
                else
                    _repo.Update(redactModel);

                return RedirectToAction("Index");
            }

            else return View(model);
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Delete(Service model)
        {
            var data = _repo.GetById(new object[] { model.Id });
            if (data == null)
                return new HttpNotFoundResult();

            _repo.Delete(model);
            return RedirectToAction("Index");
        }
    }
}