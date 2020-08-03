using Ymmv.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ymmv.Services
{
    public interface ICarStore
    {
        Task<int> AddCarAsync(Car car);
        //Task<bool> UpdateItemAsync(T item);
        //Task<bool> DeleteItemAsync(string id);
        Task<Car> GetCarAsync(int id);
        Task<IEnumerable<Car>> GetCarsAsync(bool forceRefresh = false);
    }
}
