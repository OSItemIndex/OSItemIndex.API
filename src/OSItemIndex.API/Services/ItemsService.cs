using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OSItemIndex.API.Models;
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

        public async Task<IEnumerable<OsrsBoxItem>> GetItemsAsync(ItemQuery query = null, Expression<Func<OsrsBoxItem, OsrsBoxItem>> select = null)
        {
            return await (query == null
                ? _repository.GetAllAsync(select)
                : _repository.GetAllAsync(item =>
                                              (string.IsNullOrEmpty(query.Name) || item.Name.Contains(query.Name)) &&
                                              (query.Duplicate == null || item.Duplicate == query.Duplicate) &&
                                              (query.Noted == null || item.Noted == query.Noted) &&
                                              (query.Placeholder == null || item.Placeholder == query.Placeholder) &&
                                              (query.Stackable == null || item.Stackable == query.Stackable) &&
                                              (query.TradeableOnGe == null || item.TradeableOnGe == query.TradeableOnGe),
                                          items => items.OrderBy(item => item.Id), select));
        }
    }
}
