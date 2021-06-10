using System.Collections.Generic;
using System.Threading.Tasks;
using OSItemIndex.API.Models;
using OSItemIndex.Data;

namespace OSItemIndex.API.Services
{
    public interface IItemsService
    {
        /// <summary>
        ///     Retrieves an item by ID from the repository.
        /// </summary>
        /// <param name="id">An OSRS-item ID.</param>
        Task<OsrsBoxItem> GetItemAsync(int id);

        /// <summary>
        ///     Retrieves a list of all items in the repository, and orders them ascending by their respective ID.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<OsrsBoxItem>> GetItemsAsync(ItemQuery? query = null);
    }
}
