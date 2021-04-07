using static OSItemIndex.API.Data.DbContextHelper;

namespace OSItemIndex.API.Data
{
    public interface IDbContextHelper
    {
        DbContextFactory GetFactory();
    }
}
