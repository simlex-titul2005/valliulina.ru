using Dapper;
using SX.WebCore.Abstract;
using System.Data.SqlClient;
using System.Linq;
using vru.Models;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoQuestions : SxDbRepository<int, Question, DbContext>
    {
        public override Question Create(Question model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Question>("dbo.add_question @name, @email, @phone, @text", new
                {
                    name = model.Name,
                    email = model.Email,
                    phone = model.Phone,
                    text = model.Text
                }).SingleOrDefault();

                return data;
            }
        }
    }
}