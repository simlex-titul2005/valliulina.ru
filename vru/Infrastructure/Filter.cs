using static vru.Infrastructure.HtmlHelpers.Extantions;

namespace vru.Infrastructure
{
    public sealed class Filter
    {
        public Filter()
        {
            PagerInfo = new PagerInfo(1, 10);
        }

        public Filter(int page, int pageSize)
        {
            PagerInfo = new PagerInfo(page, pageSize);
        }
        public PagerInfo PagerInfo { get; set; }
        public dynamic WhereExpressionObject { get; set; }
        public object[] AddintionalInfo { get; set; }
        public Order Order { get; set; }
        public bool? OnlyShow { get; set; }
    }
}