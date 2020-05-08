using System;
using LetkaBike.Core.Data;
using LetkaBike.Core.Repository;

namespace LetkaBike.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<City> CitiesRepository { get; }
        IRepository<Rider> RidersRepository { get; }

        void SaveChanges();
    }
}