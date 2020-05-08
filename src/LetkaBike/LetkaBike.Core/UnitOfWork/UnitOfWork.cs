using LetkaBike.Core.Data;
using LetkaBike.Core.Repository;

namespace LetkaBike.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LetkaContext _context;
        private IRepository<City> _citiesRepository;
        private IRepository<Rider> _ridersRepository;

        public UnitOfWork(LetkaContext context)
        {
            _context = context;
        }

        public IRepository<City> CitiesRepository
        {
            get { return _citiesRepository ??= new Repository<City>(_context); }
        }

        public IRepository<Rider> RidersRepository
        {
            get { return _ridersRepository ??= new Repository<Rider>(_context); }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}