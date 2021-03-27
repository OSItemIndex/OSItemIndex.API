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
        private readonly IItemsRepository _repo;

        public ItemsService(IItemsRepository itemsRepository)
        {
            _repo = itemsRepository;
        }

        public Task<OSRSBoxItem> GetItemAsync(int ID)
        {
            return _repo.GetAsync(ID);
        }

        public Task<IEnumerable<OSRSBoxItem>> GetItemsAsync()
        {
            return _repo.GetAllAsync(orderBy: o => o.OrderBy(item => item.Id));
        }

        public async Task<int> UpsertAndCommitItemsAsync(IEnumerable<OSRSBoxItem> items)
        {
            await _repo.UpsertAllAsync(items);
            return await _repo.CommitAsync();
        }
    }
}
