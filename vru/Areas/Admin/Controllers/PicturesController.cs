using SX.WebCore.MvcControllers;
using vru.Infrastructure.Repositories;

namespace vru.Areas.Admin.Controllers
{
    public sealed class PicturesController : SxPicturesController
    {
        public PicturesController()
        {
            if (Repo == null || !(Repo is RepoPicture))
                Repo = new RepoPicture();
        }
    }
}