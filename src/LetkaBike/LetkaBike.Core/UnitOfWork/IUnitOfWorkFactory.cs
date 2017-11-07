namespace LetkaBike.Core.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork InitUnitOfWork();
    }
}