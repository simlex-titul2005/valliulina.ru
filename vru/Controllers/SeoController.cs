using SX.WebCore.MvcControllers;
using vru.ViewModels;
using static SX.WebCore.Enums;

namespace vru.Controllers
{
    public class SeoController : SxSeoController
    {
        public SeoController()
        {
            SeoItemUrlFunc = SeoItemUrl;
        }

        public string SeoItemUrl(dynamic model)
        {
            var mct = (ModelCoreType)model.ModelCoreType;
            switch (mct)
            {
                case ModelCoreType.Article:
                    return new VMArticle { DateCreate = model.DateCreate, TitleUrl = model.TitleUrl }.GetUrl(Url);
                default:
                    return null;
            }
        }
    }
}