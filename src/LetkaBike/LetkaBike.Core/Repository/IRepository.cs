using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetkaBike.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}