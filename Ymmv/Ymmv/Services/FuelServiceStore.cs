using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ymmv.Models;

namespace Ymmv.Services
{
    public class FuelServiceStore : IFuelServiceStore
    {
        private readonly SQLiteAsyncConnection _db;

        public FuelServiceStore()
        {
            _db = new SQLiteAsyncConnection(Constants.DatabaseFilePath);
        }

        public async Task<int> AddFuelServiceAsync(FuelService fuelService)
        {
            await _db.InsertAsync(fuelService);

            return fuelService.Id;
        }

        public Task<FuelService> GetFuelServiceAsync(int id)
        {
            var fuelService = _db.Table<FuelService>().Where(fs => fs.Id == id).FirstOrDefaultAsync();

            return fuelService;
        }

        public Task<List<FuelService>> GetFuelServicesForCarAsync(int carId)
        {
            var fuelServices = _db.Table<FuelService>().Where(fs => fs.CarId == carId).ToListAsync();

            return fuelServices;
        }
    }
}