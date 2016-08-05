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
            var order = new SxOrder { FieldName = "ds.[Text]", Direction = SortDirection.Asc };
            var filter = new SxFilter(1, amount.HasValue ? (int)amount : maxAmount) { Order = order };
            var totalItems = 0;
            var data = _repo.Read(filter, out totalItems);
            var viewModel = data
                .Select(x => Mapper.Map<Situation, VMSituation>(x))
                .ToArray();

            return PartialView("_MainPageSituations", viewModel);
        }
    }
}