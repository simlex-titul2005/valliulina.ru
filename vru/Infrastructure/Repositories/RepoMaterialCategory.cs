using Dapper;
using SX.WebCore.DbModels;
using SX.WebCore.Repositories;
using SX.WebCore.ViewModels;
using System.Data.SqlClient;
using System.Linq;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoMaterialCategory : SxRepoMaterialCategory<SxMaterialCategory, SxVMMaterialCategory>
    {
        public override SxVMMaterialCategory[] All
        {
            get
            {
                var query = @"SELECT*FROM D_MATERIAL_CATEGORY AS dmc WHERE dmc.ParentId IS NULL ORDER BY dmc.Title";
                using (var connection = new SqlConnection(ConnectionString))
                {
                   var data = connection.Query<SxVMMaterialCategory>(query);
                   return data.ToArray();
                }
            }
        }
    }
}