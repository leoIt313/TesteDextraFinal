using System;

namespace TesteDextra.Domain.Interfaces.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
