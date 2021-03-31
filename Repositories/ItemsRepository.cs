using OSItemIndex.API.Data;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Repositories
{
    public class ItemsEntityRepository : EntityRepository<OSRSBoxItem>
    {
        public ItemsEntityRepository(IDbContextHelper dbContextHelper) : base(dbContextHelper) { }
    }
}
