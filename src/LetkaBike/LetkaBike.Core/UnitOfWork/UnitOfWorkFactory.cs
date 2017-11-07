using LetkaBike.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace LetkaBike.Core.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContextOptions _options;

        public UnitOfWorkFactory(DbContextOptions options)
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