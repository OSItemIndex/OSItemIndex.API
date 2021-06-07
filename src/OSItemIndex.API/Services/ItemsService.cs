using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSItemIndex.API.Repositories;
using OSItemIndex.Data;

namespace OSItemIndex.API.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IEntityRepository<OsrsBoxItem> _repository;

        public ItemsService(IEntityRepository<OsrsBoxItem> repository)
        {
            _repository = repository;
        }

        public async Task<OsrsBoxItem> GetItemAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<OsrsBoxItem>> GetItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<OsrsBoxItem>> GetItemsAsync(string name, bool? duplicate,
                                                                  bool? noted, bool? placeholder,
                                                                  bool? stackable, bool? tradeableOnGe)
        {
            return await _repository.GetAllAsync(item =>
                                                     (string.IsNullOrEmpty(name) || item.Name.Contains(name)) &&
                                                     (duplicate == null || item.Duplicate == duplicate) &&
                                                     (noted == null || item.Noted == noted) &&
                                                     (placeholder == null || item.Placeholder == placeholder) &&
                                                     (stackable == null || item.Stackable == stackable) &&
                                                     (tradeableOnGe == null || item.TradeableOnGe == tradeableOnGe),
                                                 items => items.OrderBy(item => item.Id));
        }
    }
}
