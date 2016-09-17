using Dapper;
using SX.WebCore.Repositories;
using SX.WebCore.ViewModels;
using System.Data.SqlClient;
using System.Linq;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoMaterialCategory : SxRepoMaterialCategory
    {
        public override SxVMMaterialCategory[] All
        {
            get
            {
                var query = @"SELECT*FROM D_MATERIAL_CATEGORY AS dmc WHERE dmc.ParentCategoryId IS NULL ORDER BY dmc.Title";
                using (var connection = new SqlConnection(ConnectionString))
                {
                   var data = connection.Query<SxVMMaterialCategory>(query);
                   return data.ToArray();
                }
            }
        }
    }
}