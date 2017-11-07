using LetkaBike.Core.Data;

namespace LetkaBike.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LetkaContext _context;

        public UnitOfWork(LetkaContext context)
        {
            _context = context;
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