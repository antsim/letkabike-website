using LetkaBike.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace LetkaBike.Core.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContextOptions<LetkaContext> _options;

        public UnitOfWorkFactory(DbContextOptions<LetkaContext> options)
        {
            _options = options;
        }

        public IUnitOfWork InitUnitOfWork()
        {
            var context = new LetkaContext(_options);
            return new UnitOfWork(context);
        }
    }
}