using OSItemIndex.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OSItemIndex.API.Repositories
{
    interface IPricesRepository<T> : IRepository<T> where T : IPriceModel
    {
        Task<T> GetPrice(object id);

        Task Update(T item);
        Task Update(IEnumerable<T> items);
    }
}
