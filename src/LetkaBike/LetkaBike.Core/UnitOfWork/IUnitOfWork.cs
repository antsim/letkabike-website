using System;

namespace LetkaBike.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}