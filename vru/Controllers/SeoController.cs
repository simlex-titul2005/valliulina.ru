using System;
using SX.WebCore.MvcControllers;
using vru.ViewModels;
using static SX.WebCore.Enums;

namespace vru.Controllers
{
    public sealed class SeoController : SxSeoController
    {
        protected override Func<dynamic, string> SeoItemUrlFunc
        {
            get
            {
                return model => { return getSeoItemUrl(model); };
            }
        }

        private string getSeoItemUrl(dynamic model)
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