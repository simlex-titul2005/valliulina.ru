using Dapper;
using SX.WebCore;
using SX.WebCore.Providers;
using SX.WebCore.Repositories.Abstract;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoSituations : SxDbRepository<int, Situation, VMSituation>
    {
        public override VMSituation[] Read(SxFilter filter)
        {
            var sb = new StringBuilder();
            sb.Append(SxQueryProvider.GetSelectString());
            sb.Append(" FROM D_SITUATION AS ds");

            object param = null;
            var gws = getSituationsWhereString(filter, out param);
            sb.Append(gws);

            var defaultOrder = new SxOrder { FieldName = "ds.[Text]", Direction = SortDirection.Desc };
            sb.Append(SxQueryProvider.GetOrderString(defaultOrder, filter.Order));

            sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

            //count
            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM D_SITUATION AS ds");
            sbCount.Append(gws);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<VMSituation>(sb.ToString(), param: param);
                filter.PagerInfo.TotalItems = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
                return data.ToArray();
            }
        }

        private static string getSituationsWhereString(SxFilter filter, out object param)
        {
            param = null;
            string query = null;
            query += " WHERE (ds.[Text] LIKE '%'+@text+'%' OR @text IS NULL) ";

            string text = filter.WhereExpressionObject?.Text;

            param = new
            {
                text = text
            };

            return query;
        }

        public override Situation GetByKey(params object[] id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Situation>("dbo.get_situation_by_id @situationId", new { situationId = id[0] }).SingleOrDefault();
                return data;
            }
        }

        public override Situation Create(Situation model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Situation>("dbo.add_situation @text", new
                {
                    text = model.Text
                }).SingleOrDefault();

                return data;
            }
        }

        public override void Delete(Situation model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute("dbo.del_situation @situationId", new { situationId = model.Id });
            }
        }

        public override Situation Update(Situation model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Situation>("dbo.update_situation @situationId, @text", new
                {
                    situationId = model.Id,
                    text = model.Text
                }).SingleOrDefault();

                return data;
            }
        }
    }
}