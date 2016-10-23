using System;
using gravel.core.DbContextFactory;

namespace gravel.core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        DapperContext Context { get; }

        void SaveChange();
        void RollBack();
    }
}