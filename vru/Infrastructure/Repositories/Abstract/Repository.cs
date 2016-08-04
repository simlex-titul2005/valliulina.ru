using System.Configuration;

namespace vru.Infrastructure.Repositories.Abstract
{
    public abstract class Repository<TModel>
    {
        protected string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
            }
        }

        public virtual TModel Create(TModel model)
        {
            return default(TModel);
        }

        public virtual TModel[] Read(Filter filter, out int allCount)
        {
            allCount = 0;
            return null;
        }

        public virtual TModel Update(TModel model)
        {
            return default(TModel);
        }

        public virtual void Delete(TModel model)
        {
        }

        public virtual TModel GetById (object[] id)
        {
            return default(TModel);
        }
    }
}