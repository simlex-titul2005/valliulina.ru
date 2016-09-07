using Dapper;
using SX.WebCore;
using SX.WebCore.Providers;
using SX.WebCore.Repositories;
using SX.WebCore.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoArticles : SxRepoMaterial<Article, VMArticle, DbContext>
    {
        public RepoArticles() : base(Enums.ModelCoreType.Article) { }

        public override VMArticle[] Read(SxFilter filter)
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
                { "Title", "dm.Title"},
                { "FilterCategoryTitle","dmc.Title"}
            }));

            sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

            //count
            var sbCount = new StringBuilder();
            sbCount.AppendFormat(@"SELECT COUNT(1) {0}", query);
            sbCount.Append(gws);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<VMArticle, SxVMPicture, SxVMAppUser, SxVMSeoTags, SxVMMaterialCategory, VMArticle>(sb.ToString(), (a, p, u, t, c) =>
                {
                    a.FrontPicture = p;
                    a.User = u;
                    a.SeoTags = t;
                    a.Category = c;
                    return a;
                }, param: param);
                filter.PagerInfo.TotalItems = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
                return data.ToArray();
            }
        }
        private static string getArticlesWhereString(SxFilter filter, out object param)
        {
            param = null;
            string query = null;
            query += " WHERE (dm.Title LIKE '%'+@title+'%' OR @title IS NULL) ";
            query += " AND (dm.Foreword LIKE '%'+@fwd+'%' OR @fwd IS NULL) ";
            query += " AND (dmc.Title LIKE '%'+@cTitle+'%' OR @cTitle IS NULL) ";
            query += " AND (dm.Show=1 AND dm.DateOfPublication<=GETDATE()) ";
            query += " AND (dm.CategoryId LIKE '%'+@cat+'%' OR @cat IS NULL) ";

            var title = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Title != null ? (string)filter.WhereExpressionObject.Title : null;
            var fwd = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Foreword != null ? (string)filter.WhereExpressionObject.Foreword : null;
            var cTitle = filter.AddintionalInfo != null && filter.AddintionalInfo[0] != null ? (string)filter.AddintionalInfo[0] : null;
            var cat = filter.WhereExpressionObject != null && filter.WhereExpressionObject.CategoryId != null ? (string)filter.WhereExpressionObject.CategoryId : null;

            param = new
            {
                title = title,
                fwd = fwd,
                cTitle = cTitle,
                cat = cat
            };

            return query;
        }
    }
}