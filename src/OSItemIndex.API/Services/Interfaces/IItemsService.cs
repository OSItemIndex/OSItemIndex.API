using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>Ascending ordered enumerable by OSRSBoxItem.ID</returns>
        Task<IEnumerable<OsrsBoxItem>> GetItemsAsync();

        /// <summary>
        ///     Finds all items within the database that contains the passed string
        /// </summary>
        /// <param name="name"></param>
        /// <param name="duplicate"></param>
        /// <param name="noted"></param>
        /// <param name="placeholder"></param>
        /// <param name="stackable"></param>
        /// <param name="tradeableOnGe"></param>
        /// <returns></returns>
        Task<IEnumerable<OsrsBoxItem>> GetItemsAsync(string name, bool? duplicate,
                                                     bool? noted, bool? placeholder,
                                                     bool? stackable, bool? tradeableOnGe);
    }
}
