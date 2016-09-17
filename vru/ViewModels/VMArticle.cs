using System.Web.Mvc;
using SX.WebCore.ViewModels;

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
            return urlHelper.Action("Details", "Articles", new { year = DateCreate.Year, month = DateCreate.Month.ToString("00"), day = DateCreate.Day.ToString("00"), titleUrl = TitleUrl });
        }
    }
}