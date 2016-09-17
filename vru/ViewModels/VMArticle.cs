using System.Web.Mvc;
using SX.WebCore.ViewModels;
using SX.WebCore;

namespace vru.ViewModels
{
    public sealed class VMArticle : SxVMMaterial
    {
        public string FilterCategoryTitle
        {
            get
            {
                return Category?.Title;
            }
            set
            {
                
            }
        }

        public sealed override string GetUrl(UrlHelper urlHelper)
        {
            switch (ModelCoreType)
            {
                case Enums.ModelCoreType.Article:
                    return urlHelper.Action("Details", "Articles", new { year = DateCreate.Year, month = DateCreate.Month.ToString("00"), day = DateCreate.Day.ToString("00"), titleUrl = TitleUrl });
                case Enums.ModelCoreType.News:
                    return urlHelper.Action("Details", "News", new { year = DateCreate.Year, month = DateCreate.Month.ToString("00"), day = DateCreate.Day.ToString("00"), titleUrl = TitleUrl });
                default:
                    return "#";
            }
        }
    }
}