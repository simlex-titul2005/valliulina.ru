using Dapper;
using SX.WebCore;
using SX.WebCore.Abstract;
using SX.WebCore.Providers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using vru.Models;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoArticles : SxDbRepository<int, Article, DbContext>
    {
        public override Article[] Read(SxFilter filter, out int allCount)
        {
            var query = @" FROM  D_ARTICLE AS da
       JOIN DV_MATERIAL               AS dm
            ON  dm.Id = da.Id
            AND dm.ModelCoreType = da.ModelCoreType
       LEFT JOIN D_PICTURE            AS dp
            ON  dp.Id = dm.FrontPictureId
       LEFT JOIN AspNetUsers          AS anu
            ON  anu.Id = dm.UserId
       LEFT JOIN D_SEO_TAGS           AS dst
            ON  dst.MaterialId = dm.Id
            AND dst.ModelCoreType = dm.ModelCoreType
       LEFT JOIN D_MATERIAL_CATEGORY  AS dmc
            ON  dmc.Id = dm.CategoryId ";

            var sb = new StringBuilder();
            sb.Append(SxQueryProvider.GetSelectString());
            sb.Append(query);

            object param = null;
            var gws = getArticlesWhereString(filter, out param);
            sb.Append(gws);

            var defaultOrder = new SxOrder { FieldName = "dm.DateCreate", Direction = SortDirection.Desc };
            sb.Append(SxQueryProvider.GetOrderString(defaultOrder, filter.Order, new Dictionary<string, string> {
                { "Title", "dm.Title"}
            }));

            sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

            //count
            var sbCount = new StringBuilder();
            sbCount.AppendFormat(@"SELECT COUNT(1) {0}", query);
            sbCount.Append(gws);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Article>(sb.ToString(), param: param);
                allCount = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
                return data.ToArray();
            }
        }

        private static string getArticlesWhereString(SxFilter filter, out object param)
        {
            param = null;
            string query = null;
            query += " WHERE (dm.Title LIKE '%'+@title+'%' OR @title IS NULL) ";

            var title = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Title != null ? (string)filter.WhereExpressionObject.Title : null;

            param = new
            {
                title = title
            };

            return query;
        }
    }
}