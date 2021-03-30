using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OSItemIndex.API.Models;

namespace OSItemIndex.API.Services
{
    public interface IItemsService
    {
        /// <summary>
        ///     Retrieves an item by ID from the repository.
        /// </summary>
        /// <param name="ID">An OSRS-item ID.</param>
        Task<OSRSBoxItem> GetItemAsync(int ID);

        /// <summary>
        ///     Retrieves a collection of all items in the repository, and orders them ascendingly by their respective ID.
        /// </summary>
        /// <returns>Ascending ordered enumerable by OSRSBoxItem.ID</returns>
        Task<IEnumerable<OSRSBoxItem>> GetItemsAsync();

        Task<int> CountItemsAsync();
        Task<int> CountItemsWithNamesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> UpsertAndCommitItemsAsync(IEnumerable<OSRSBoxItem> items);
    }
}
