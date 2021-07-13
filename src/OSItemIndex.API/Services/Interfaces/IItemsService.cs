using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OSItemIndex.API.Models;
using OSItemIndex.Data;

namespace OSItemIndex.API.Services
{
    public interface IItemsService
    {
        Task<OsrsBoxItem> GetItemAsync(int id);
        Task<IEnumerable<OsrsBoxItem>> GetItemsAsync(ItemQuery query = null, Expression<Func<OsrsBoxItem, OsrsBoxItem>> select = null);
    }
}
