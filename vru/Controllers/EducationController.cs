using SX.WebCore;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Controllers
{
    public sealed class EducationController : BaseController
    {
        private static RepoEducation _repo;
        public EducationController()
        {
            if (_repo == null)
                _repo = new RepoEducation();
        }

        private readonly int _pageSize = 10;

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var order = new SxOrder { FieldName = "de.[Order]", Direction = SortDirection.Desc };
            var filter = new SxFilter(page, _pageSize) { Order = order };

            var viewData = _repo.Read(filter);
            if (page > 1 && !viewData.Any())
                return new HttpNotFoundResult();

            var viewModel = new SxPagedCollection<VMEducation>
            {
                Collection = viewData,
                PagerInfo = filter.PagerInfo
            };

            ViewBag.Filter = filter;

            return View(viewModel);
        }
    }
}