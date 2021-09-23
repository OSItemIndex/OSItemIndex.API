namespace OSItemIndex.API.Repositories
{
    public class PricesRepository : EntityRepository<Real>
    {
        public ItemsRepository(IDbContextHelper context) : base(context) { }
    }
}
