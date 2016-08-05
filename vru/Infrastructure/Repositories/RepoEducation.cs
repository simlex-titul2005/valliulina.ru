using Dapper;
using SX.WebCore;
using SX.WebCore.Abstract;
using SX.WebCore.Providers;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using vru.Models;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoEducation : SxDbRepository<int, Education, DbContext>
    {
        public override Education[] Read(SxFilter filter, out int allCount)
        {
            var sb = new StringBuilder();
            sb.Append(SxQueryProvider.GetSelectString());
            sb.Append(" FROM D_EDUCATION AS de");

            object param = null;
            var gws = getEducationWhereString(filter, out param);
            sb.Append(gws);

            var defaultOrder = new SxOrder { FieldName = "de.[YEAR]", Direction = SortDirection.Desc };
            sb.Append(SxQueryProvider.GetOrderString(defaultOrder, filter.Order));

            sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

            //count
            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM D_EDUCATION AS de");
            sbCount.Append(gws);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Education>(sb.ToString(), param: param);
                allCount = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
                return data.ToArray();
            }
        }

        private static string getEducationWhereString(SxFilter filter, out object param)
        {
            param = null;
            string query = null;
            query += " WHERE (de.Html LIKE '%'+@html+'%' OR @html IS NULL) ";

            var html = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Html != null ? (string)filter.WhereExpressionObject.Html : null;

            param = new
            {
                html = html
            };

            return query;
        }

        public override Education Create(Education model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Education>("dbo.add_education @year, @month, @groupName, @html, @pictureId", new
                {
                    year=model.Year,
                    month=model.Month,
                    groupName=model.GroupName,
                    html=model.Html,
                    pictureId=model.PictureId
                }).SingleOrDefault();

                return data;
            }
        }

        public override void Delete(Education model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute("dbo.del_education @educationId", new { educationId = model.Id });
            }
        }

        public override Education Update(Education model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Education>("dbo.update_education @educationId, @year, @month, @groupName, @html, @pictureId", new
                {
                    educationId=model.Id,
                    year = model.Year,
                    month = model.Month,
                    groupName = model.GroupName,
                    html = model.Html,
                    pictureId = model.PictureId
                }).SingleOrDefault();

                return data;
            }
        }
    }
}