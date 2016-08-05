using Dapper;
using SX.WebCore;
using SX.WebCore.Abstract;
using SX.WebCore.Providers;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vru.Models;
using static SX.WebCore.HtmlHelpers.SxExtantions;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoQuestions : SxDbRepository<int, Question, DbContext>
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

        public override Question[] Read(SxFilter filter, out int allCount)
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
                var data = conn.Query<Question>(sb.ToString(), param: param);
                allCount = conn.Query<int>(sbCount.ToString(), param: param).SingleOrDefault();
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

            var name = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Name != null ? (string)filter.WhereExpressionObject.Name : null;
            var email = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Email != null ? (string)filter.WhereExpressionObject.Email : null;
            var phone = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Phone != null ? (string)filter.WhereExpressionObject.Phone : null;
            var text = filter.WhereExpressionObject != null && filter.WhereExpressionObject.Text != null ? (string)filter.WhereExpressionObject.Text : null;

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