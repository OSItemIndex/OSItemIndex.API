using static OSItemIndex.API.DbContextHelper;

namespace OSItemIndex.API
{
    public interface IDbContextHelper
    {
        DbContextFactory GetFactory();
    }
}
