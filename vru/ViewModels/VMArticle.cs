using System.Web.Mvc;
using vru.ViewModels.Abstract;

namespace vru.ViewModels
{
    public sealed class VMArticle : VMMaterial
    {
        public override string GetUrl(UrlHelper urlHelper)
        {
            return urlHelper.Action("Details", "Articles", new { year = DateCreate.Year, month = DateCreate.Month.ToString("00"), day = DateCreate.Day.ToString("00"), titleUrl = TitleUrl });
        }

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
    }
}