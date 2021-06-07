using OSItemIndex.Data;
using OSItemIndex.Data.Database;

namespace OSItemIndex.API.Repositories
{
    public class ItemRepository : EntityRepository<OsrsBoxItem>
    {
        public ItemRepository(IDbContextHelper context) : base(context) { }
    }
}
