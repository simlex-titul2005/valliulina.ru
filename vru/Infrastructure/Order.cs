using static vru.Infrastructure.HtmlHelpers.Extantions;

namespace vru.Infrastructure
{
    public sealed class Order
    {
        public string FieldName { get; set; }
        public SortDirection Direction { get; set; }
    }
}