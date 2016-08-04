using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static vru.Infrastructure.HtmlHelpers.Extantions;

namespace vru.Controllers
{
    public sealed class ServicesController : BaseController
    {
        private static RepoServices _repo;
        public ServicesController()
        {
            if (_repo == null)
                _repo = new RepoServices();
        }

        private readonly int _pageSize = 10;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var order = new Order { FieldName = "Title", Direction = SortDirection.Asc };
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
    }
}