using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSItemIndex.API.Models;
using OSItemIndex.API.Repositories;

namespace OSItemIndex.API.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IEntityRepository<OSRSBoxItem> _itemsEntityRepository;

        public ItemsService(IEntityRepository<OSRSBoxItem> itemsEntityRepository)
        {
            _itemsEntityRepository = itemsEntityRepository;
        }

        public Task<OSRSBoxItem> GetItemAsync(int id)
        {
            return _itemsEntityRepository.GetAsync(id);
        }

        public Task<IEnumerable<OSRSBoxItem>> GetItemsAsync()
        {
            return _itemsEntityRepository.GetAllAsync(null, orderBy: o => o.OrderBy(item => item.Id));
        }

        public Task<int> CountItemsAsync()
        {
            return _itemsEntityRepository.CountAsync();
        }

        public async Task<int> UpsertAndCommitItemsAsync(IEnumerable<OSRSBoxItem> items)
        {
            await _itemsEntityRepository.UpsertRangeAsync(items);
            return await _itemsEntityRepository.CommitAsync();
        }
    }
}
