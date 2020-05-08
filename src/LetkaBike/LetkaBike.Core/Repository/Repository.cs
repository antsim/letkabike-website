using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LetkaBike.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace LetkaBike.Core.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly LetkaContext _context;

        public Repository(LetkaContext context)
        {
            _context = context;
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public TEntity GetFirstWith(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entityQuery = _context.Set<TEntity>().Where(predicate);
            foreach (var includeProperty in includeProperties) entityQuery = entityQuery.Include(includeProperty);
            return entityQuery.FirstOrDefault();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Update(entity);
        }
    }
}