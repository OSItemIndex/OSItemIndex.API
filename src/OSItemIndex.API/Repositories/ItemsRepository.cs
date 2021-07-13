using OSItemIndex.Data;
using OSItemIndex.Data.Database;

namespace OSItemIndex.API.Repositories
{
    public class ItemsRepository : EntityRepository<OsrsBoxItem>
    {
        public ItemsRepository(IDbContextHelper context) : base(context) { }
    }
}
