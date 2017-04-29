using Dapper;
using SX.WebCore.MvcApplication;
using SX.WebCore.SxRepositories;
using SX.WebCore.ViewModels;
using System.Data.SqlClient;
using System.Linq;
using vru.Models;
using vru.ViewModels;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoArticles : SxRepoMaterial<Article, VMArticle>
    {
        public RepoArticles() : base(SxMvcApplication.ModelCoreTypeProvider[nameof(Article)]) { }

       // public override VMArticle[] Read(SxFilter filter)
       // {
       //     var query = @" FROM  D_ARTICLE AS da
       //JOIN DV_MATERIAL               AS dm
       //     ON  dm.Id = da.Id
       //     AND dm.ModelCoreType = da.ModelCoreType
       //LEFT JOIN D_PICTURE            AS dp
       //     ON  dp.Id = dm.FrontPictureId
       //LEFT JOIN AspNetUsers          AS anu
       //     ON  anu.Id = dm.UserId
       //LEFT JOIN D_SEO_TAGS           AS dst
       //     ON  dst.MaterialId = dm.Id
       //     AND dst.ModelCoreType = dm.ModelCoreType
       //LEFT JOIN D_MATERIAL_CATEGORY  AS dmc
       //     ON  dmc.Id = dm.CategoryId ";

       //     var sb = new StringBuilder();
       //     sb.Append(SxQueryProvider.GetSelectString());
       //     sb.Append(query);

       //     object param = null;
       //     var gws = getArticlesWhereString(filter, out param);
       //     sb.Append(gws);

       //     var defaultOrder = new SxOrder { FieldName = "dm.DateCreate", Direction = SortDirection.Desc };
       //     sb.Append(SxQueryProvider.GetOrderString(defaultOrder, filter.Order, new Dictionary<string, string> {
       //         { "Title", "dm.Title"},
       //         { "FilterCategoryTitle","dmc.Title"}
       //     }));

       //     sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

       //     //count
       //     var sbCount = new StringBuilder();
       //     sbCount.AppendFormat(@"SELECT COUNT(1) {0}", query);
       //     sbCount.Append(gws);

       //     using (var conn = new SqlConnection(ConnectionString))
       //     {
       //         var data = conn.Query<VMArticle, SxVMPicture, SxVMAppUser, SxVMSeoTags, SxVMMaterialCategory, VMArticle>(sb.ToString(), (a, p, u, t, c) =>
       //         {
       //             a.FrontPicture = p;
       //             a.User = u;
       //             a.SeoTags = t;
       //             a.Category = c;
       //             return a;
       //         }, param: param);

       //         filter.PagerInfo.TotalItems = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
       //         return data.ToArray();
       //     }
       // }

//        public override Article GetByKey(params object[] id)
//        {
//            var query = @"SELECT *
//FROM   DV_MATERIAL                    AS dm
//       LEFT JOIN D_PICTURE            AS dp
//            ON  dp.Id = dm.FrontPictureId
//       LEFT JOIN AspNetUsers          AS anu
//            ON  anu.Id = dm.UserId
//       LEFT JOIN D_SEO_TAGS           AS dst
//            ON  dst.MaterialId = dm.Id
//            AND dst.ModelCoreType = dm.ModelCoreType
//       LEFT JOIN D_MATERIAL_CATEGORY  AS dmc
//            ON  dmc.Id = dm.CategoryId
//WHERE  dm.Id = @id
//       AND dm.ModelCoreType = @mct";

//            using (var connection = new SqlConnection(ConnectionString))
//            {
//                var data = connection.Query<Article, SxPicture, SxAppUser, SxSeoTags, SxMaterialCategory, Article>(query, (a, p, u, t, c) =>
//                {
//                    a.FrontPicture = p;
//                    a.User = u;
//                    a.SeoTags = t;
//                    a.Category = c;
//                    return a;
//                }, param: new
//                {
//                    id = id[0],
//                    mct = id[1]
//                }, splitOn: "Id");

//                return data.SingleOrDefault();
//            }

//        }

        //private static string getArticlesWhereString(SxFilter filter, out object param)
        //{
        //    param = null;
        //    string query = null;
        //    query += " WHERE (dm.Title LIKE '%'+@title+'%' OR @title IS NULL) ";
        //    query += " AND (dm.Foreword LIKE '%'+@fwd+'%' OR @fwd IS NULL) ";
        //    query += " AND (dmc.Title LIKE '%'+@cTitle+'%' OR @cTitle IS NULL) ";
        //    query += " AND (dm.Show=1 AND dm.DateOfPublication<=GETDATE()) ";
        //    query += " AND (dm.CategoryId LIKE '%'+@cat+'%' OR @cat IS NULL) ";

        //    string title = filter.WhereExpressionObject?.Title;
        //    string fwd = filter.WhereExpressionObject?.Foreword;
        //    string cTitle = filter.AddintionalInfo != null && filter.AddintionalInfo[0] != null ? (string)filter.AddintionalInfo[0] : null;
        //    string cat = filter.WhereExpressionObject?.CategoryId;

        //    param = new
        //    {
        //        title = title,
        //        fwd = fwd,
        //        cTitle = cTitle,
        //        cat = cat
        //    };

        //    return query;
        //}

        public override void Delete(Article model)
        {
            var query = @"DELETE 
FROM   D_ARTICLE
WHERE  Id = @id
       AND ModelCoreType = @mct

BEGIN TRANSACTION
DELETE 
FROM   DV_MATERIAL
WHERE  Id = @id
       AND ModelCoreType = @mct

COMMIT TRANSACTION
";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var data = connection.Execute(query, new { id = model.Id, mct = model.ModelCoreType });
            }
        }

        public VMArticle GetByTitleUrl(int year, string month, string day, string titleUrl)
        {
            var query = @"SELECT *
FROM   DV_MATERIAL                    AS dm
       LEFT JOIN D_PICTURE            AS dp
            ON  dp.Id = dm.FrontPictureId
       LEFT JOIN AspNetUsers          AS anu
            ON  anu.Id = dm.UserId
       LEFT JOIN D_SEO_TAGS           AS dst
            ON  dst.MaterialId = dm.Id
            AND dst.ModelCoreType = dm.ModelCoreType
       LEFT JOIN D_MATERIAL_CATEGORY  AS dmc
            ON  dmc.Id = dm.CategoryId
WHERE  YEAR(dm.DateCreate)=@year AND MONTH(dm.DateCreate)=@month AND DAY(dm.DateCreate)=@day
       AND dm.TitleUrl = @titleUrl
       AND dm.Show=1 AND dm.DateOfPublication<=GETDATE()";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var data = connection.Query<VMArticle, SxVMPicture, SxVMAppUser, SxVMSeoTags, SxVMMaterialCategory, VMArticle>(query, (a, p, u, t, c) =>
                {
                    a.FrontPicture = p;
                    a.User = u;
                    a.SeoTags = t;
                    a.Category = c;
                    return a;
                }, param: new
                {
                    year = year,
                    month = month,
                    day=day,
                    titleUrl=titleUrl
                }, splitOn: "Id");

                return data.SingleOrDefault();
            }
        }
    }
}