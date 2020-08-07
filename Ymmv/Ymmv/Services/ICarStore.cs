using Ymmv.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ymmv.Services
{
    public interface ICarStore
    {
        Task<int> AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
        Task<Car> GetCarAsync(int id);
        Task<List<Car>> GetCarsAsync();
    }
}
