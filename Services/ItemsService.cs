using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSItemIndex.API.Models;
using OSItemIndex.API.Repositories;

namespace OSItemIndex.API.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsService(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public Task<OSRSBoxItem> GetItemAsync(int ID)
        {
            return _itemsRepository.GetAsync(ID);
        }

        public Task<IEnumerable<OSRSBoxItem>> GetItemsAsync()
        {
            return _itemsRepository.GetAllAsync(orderBy: o => o.OrderBy(item => item.Id));
        }

        public Task<int> CountItemsAsync()
        {
            return _itemsRepository.CountAsync();
        }

        public Task<int> CountItemsWithNamesAsync()
        {
            return _itemsRepository.CountAsync(item => item.Name != null);
        }

        public async Task<int> UpsertAndCommitItemsAsync(IEnumerable<OSRSBoxItem> items)
        {
            await _itemsRepository.UpsertAllAsync(items);
            return await _itemsRepository.CommitAsync();
        }
    }
}
