using SX.WebCore;
using SX.WebCore.MvcControllers;
using System;
using System.Web.Mvc;
using vru.Infrastructure;
using vru.Infrastructure.Repositories;

namespace vru.Areas.Admin.Controllers
{
    public sealed class PicturesController : SxPicturesController<DbContext>
    {
        private static RepoPicture _repo;
        public PicturesController()
        {
            if (_repo == null)
                _repo = new RepoPicture();
        }

        public override ActionResult Delete(Guid id)
        {
            _repo.Delete(new SxPicture { Id=id});
            return RedirectToAction("Index", "Pictures");
        }
    }
}