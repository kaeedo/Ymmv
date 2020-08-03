using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ymmv.Models;

namespace Ymmv.Services
{
    public class InMemoryCarStore : ICarStore
    {
        readonly List<Car> cars;

        public InMemoryCarStore()
        {
            cars = new List<Car>();
        }

        public Task<int> AddCarAsync(Car car)
        {
            car.Id = cars.Count + 1;
            cars.Add(car);

            return Task.FromResult(car.Id);
        }

        public Task<Car> GetCarAsync(int id)
        {
            return Task.FromResult(cars.FirstOrDefault(c => c.Id == id));
        }

        public Task<IEnumerable<Car>> GetCarsAsync(bool forceRefresh = false)
        {
            return Task.FromResult<IEnumerable<Car>>(cars);
        }
    }
}