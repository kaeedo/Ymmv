using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ymmv.Models;

namespace Ymmv.Services
{
    public class CarStore : ICarStore
    {
        private readonly SQLiteAsyncConnection _db;

        public CarStore()
        {
            _db = new SQLiteAsyncConnection(Constants.DatabaseFilePath);
        }

        public async Task<int> AddCarAsync(Car car)
        {
            await _db.InsertAsync(car);

            return car.Id;
        }

        public Task UpdateCarAsync(Car car)
        {
            return _db.UpdateAsync(car);
        }

        public Task DeleteCarAsync(int id)
        {
            return _db.DeleteAsync<Car>(id);
        }

        public Task<Car> GetCarAsync(int id)
        {
            var car = _db.Table<Car>().Where(c => c.Id == id).FirstOrDefaultAsync();

            return car;
        }

        public Task<List<Car>> GetCarsAsync()
        {
            var cars = _db.Table<Car>().ToListAsync();

            return cars;
        }
    }
}