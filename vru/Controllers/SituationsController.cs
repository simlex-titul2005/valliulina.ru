using SX.WebCore;
using System.Linq;
using System.Web.Mvc;
using vru.Infrastructure.Repositories;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Controllers
{
    public sealed class SituationsController : BaseController
    {
        private static RepoSituations _repo;
        public SituationsController()
        {
            if (_repo == null)
                _repo = new RepoSituations();
        }

#if !DEBUG
[OutputCache(Duration =900)]
#endif
        [HttpGet, ChildActionOnly]
        public PartialViewResult MainPageSituations(int? amount = null, int maxAmount=30)
        {
            var order = new SxOrderItem { FieldName = "ds.[Text]", Direction = SortDirection.Asc };
            var filter = new SxFilter(1, amount.HasValue ? (int)amount : maxAmount) { Order = order };
            var viewModel = _repo.Read(filter);

            return PartialView("_MainPageSituations", viewModel);
        }
    }
}