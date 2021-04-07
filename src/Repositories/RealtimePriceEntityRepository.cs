using OSItemIndex.API.Data;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Repositories
{
    public class RealtimePriceEntityRepository : EntityRepository<RealtimePrice>
    {
        public RealtimePriceEntityRepository(IDbContextHelper dbContextHelper) : base(dbContextHelper) { }
    }
}
