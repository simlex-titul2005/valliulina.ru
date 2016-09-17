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
    }
}