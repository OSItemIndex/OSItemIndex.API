using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSItemIndex.API.Models;
using OSItemIndex.API.Repositories;

namespace OSItemIndex.API.Services
{
    public class RealtimePricesService : IRealtimePricesService
    {
        private readonly IEntityRepository<RealtimePrice> _pricesEntityRepository;

        public RealtimePricesService(IEntityRepository<RealtimePrice> pricesEntityRepository)
        {
            _pricesEntityRepository = pricesEntityRepository;
        }

        public Task<RealtimePrice> GetPriceAsync(int id)
        {
            return _pricesEntityRepository.GetAsync(id);
        }

        public Task<IEnumerable<RealtimePrice>> GetPricesAsync()
        {
            return _pricesEntityRepository.GetAllAsync(null, orderBy: o => o.OrderBy(price => price.Id));
        }

        public Task<int> CountPricesAsync()
        {
            return _pricesEntityRepository.CountAsync();
        }

        public async Task<int> UpsertAndCommitPricesAsync(IEnumerable<RealtimePrice> prices)
        {
            await _pricesEntityRepository.UpsertRangeAsync(prices);
            return await _pricesEntityRepository.CommitAsync();
        }
    }
}
