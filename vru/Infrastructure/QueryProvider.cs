using System.Collections.Generic;
using System.Text;

namespace vru.Infrastructure
{
    public static class QueryProvider
    {
        public static string GetSelectString(string[] columns = null)
        {
            var sb = new StringBuilder();
            if (columns == null)
            {
                sb.Append(",* ");
            }
            else
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    var column = columns[i];
                    sb.Append("," + column);
                }
            }
            var s = sb.ToString();
            return "SELECT " + s.Substring(1);
        }

        public static string GetOrderString(Order defaultOrder, Order order = null, Dictionary<string, string> replaceList = null)
        {
            var sb = new StringBuilder();
            if (order == null || order.FieldName == null)
                sb.AppendFormat(",{0} {1}", defaultOrder.FieldName, defaultOrder.Direction.ToString().ToUpper());
            else
                sb.AppendFormat(",{0} {1}", replaceList != null && replaceList.ContainsKey(order.FieldName) ? replaceList[order.FieldName] : order.FieldName, order.Direction.ToString().ToUpper());

            sb.Remove(0, 1);
            return "ORDER BY " + sb.ToString();
        }
    }
}