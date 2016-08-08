using vru.ViewModels.Abstract;

namespace vru.ViewModels
{
    public sealed class VMArticle : VMMaterial
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