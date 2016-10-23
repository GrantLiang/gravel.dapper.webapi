

namespace gravel.core.DbContextFactory
{
    public interface IDbContextFactory
    {
        DapperContext GetDbContext();
    }
}