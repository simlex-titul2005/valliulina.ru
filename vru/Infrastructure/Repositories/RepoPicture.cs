using Dapper;
using SX.WebCore;
using SX.WebCore.Repositories;
using System.Data.SqlClient;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoPicture : SxRepoPicture<DbContext>
    {
        public override void Delete(SxPicture model)
        {
            var query = "DELETE FROM D_PICTURE WHERE Id=@id";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(query, new { id=model.Id });
            }
        }
    }
}