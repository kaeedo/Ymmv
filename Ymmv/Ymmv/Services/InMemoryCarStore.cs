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
            cars = new List<Car>()
            {
                //new Car{Id=1,Make="Honda", Model="CR-V", FuelServices = new List<FuelService>
                //    {
                //        new FuelService{Id=1,Liters=23.4,Kilometers=241.5,ServiceDate=DateTime.Parse("2020-07-1")}
                //    }
                //},
                //new Car{Id=2,Make="Mercedes", Model="E", FuelServices = new List<FuelService>
                //    {
                //        new FuelService{Id=1,Liters=23.4,Kilometers=241.5,ServiceDate=DateTime.Parse("2020-07-1")}
                //    }
                //},
                //new Car{Id=3,Make="Audi", Model="A6",
                //    FuelServices = new List<FuelService>
                //    {
                //        new FuelService{Id=1,Liters=23.4,Kilometers=241.5,ServiceDate=DateTime.Parse("2020-07-1")}
                //    }
                //}
            };
        }

        public Task<bool> AddCarAsync(Car car)
        {
            car.Id = cars.Count + 1;
            cars.Add(car);

            return Task.FromResult(true);
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