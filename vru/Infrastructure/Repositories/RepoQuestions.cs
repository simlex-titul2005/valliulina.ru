using Dapper;
using SX.WebCore;
using SX.WebCore.Providers;
using SX.WebCore.Repositories.Abstract;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vru.Models;
using vru.ViewModels;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoQuestions : SxDbRepository<int, Question, VMQuestion>
    {
        public override Question GetByKey(params object[] id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<Question>("dbo.get_question_by_id @questionId", new
                {
                    questionId = id[0]
                }).SingleOrDefault();

                return data;
            }
        }

        public async Task UpdateReadStatus(int questionId, bool read)
        {
            await Task.Run(() =>
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var data = conn.Execute("dbo.update_question_read @questionId, @read", new
                    {
                        questionId = questionId,
                        read = read
                    });
                }
            });
        }

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

        public override VMQuestion[] Read(SxFilter filter)
        {
            var sb = new StringBuilder();
            sb.Append(SxQueryProvider.GetSelectString());
            sb.Append(" FROM D_QUESTION AS dq");

            object param = null;
            var gws = getQuestionsWhereString(filter, out param);
            sb.Append(gws);

            var defaultOrder = new SxOrder { FieldName = "dq.DateCreate", Direction = SortDirection.Desc };
            sb.Append(SxQueryProvider.GetOrderString(defaultOrder, filter.Order));

            sb.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", filter.PagerInfo.SkipCount, filter.PagerInfo.PageSize);

            //count
            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM D_QUESTION AS dq");
            sbCount.Append(gws);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var data = conn.Query<VMQuestion>(sb.ToString(), param: param);
                filter.PagerInfo.TotalItems = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
                return data.ToArray();
            }
        }

        private static string getQuestionsWhereString(SxFilter filter, out object param)
        {
            param = null;
            string query = null;
            query += " WHERE (dq.[Name] LIKE '%'+@name+'%' OR @name IS NULL) ";
            query += " AND (dq.[Email] LIKE '%'+@email+'%' OR @email IS NULL) ";
            query += " AND (dq.[Phone] LIKE '%'+@phone+'%' OR @phone IS NULL) ";
            query += " AND (dq.[Text] LIKE '%'+@text+'%' OR @text IS NULL) ";

            string name = filter.WhereExpressionObject?.Name;
            string email = filter.WhereExpressionObject?.Email;
            string phone = filter.WhereExpressionObject?.Phone;
            string text = filter.WhereExpressionObject?.Text;

            param = new
            {
                name = name,
                email = email,
                phone = phone,
                text = text
            };

            return query;
        }

        public override void Delete(Question model)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute("dbo.del_question @questionId", new
                {
                    questionId = model.Id
                });
            }
        }
    }
}