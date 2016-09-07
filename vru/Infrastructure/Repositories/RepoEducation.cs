using Dapper;
using SX.WebCore;
using SX.WebCore.Abstract;
using SX.WebCore.Providers;
using SX.WebCore.ViewModels;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoEducation : SxDbRepository<int, Education, DbContext, VMEducation>
    {
        public override Education GetByKey(params object[] id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Education, SxPicture, Education>("dbo.get_education_by_key @educationId", (e, p) => {
                    e.Picture = p;
                    return e;
                }, new
                {
                    educationId = id[0]
                }).SingleOrDefault();

                return data;
            }
        }

        public override VMEducation[] Read(SxFilter filter)
        {
            var sb = new StringBuilder();
            sb.Append(SxQueryProvider.GetSelectString());
            sb.Append(" FROM D_EDUCATION AS de LEFT JOIN D_PICTURE AS dp ON dp.Id=de.PictureId ");

            object param = null;
            var gws = getEducationWhereString(filter, out param);
            sb.Append(gws);

            var defaultOrder = new SxOrder { FieldName = "Order", Direction = SortDirection.Desc };
            sb.Append(SxQueryProvider.GetOrderString(defaultOrder, filter.Order, new System.Collections.Generic.Dictionary<string, string> {
                { "Order", "de.[Order]"}
            }));

            sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

            //count
            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM D_EDUCATION AS de");
            sbCount.Append(gws);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<VMEducation, SxVMPicture, VMEducation>(sb.ToString(), (e,p)=> {
                    e.Picture = p;
                    return e;
                }, param: param, splitOn:"Id");
                filter.PagerInfo.TotalItems = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
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
                var data = conn.Query<Education, SxPicture, Education>("dbo.add_education @year, @month, @groupName, @html, @pictureId, @order", (e,p)=> {
                    e.Picture = p;
                    return e;
                }, new
                {
                    year=model.Year,
                    month=model.Month,
                    groupName=model.GroupName,
                    html=model.Html,
                    pictureId=model.PictureId,
                    order=model.Order
                }, splitOn: "Id").SingleOrDefault();

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
                var data = conn.Query<Education, SxPicture, Education>("dbo.update_education @educationId, @year, @month, @groupName, @html, @pictureId, @order", (e, p)=> {
                    e.Picture = p;
                    return e;
                }, new
                {
                    educationId=model.Id,
                    year = model.Year,
                    month = model.Month,
                    groupName = model.GroupName,
                    html = model.Html,
                    pictureId = model.PictureId,
                    order=model.Order
                }, splitOn: "Id").SingleOrDefault();

                return data;
            }
        }
    }
}