using System.Collections.Generic;
using System.Threading.Tasks;
using Ymmv.Models;

namespace Ymmv.Services
{
    public interface IFuelServiceStore
    {
        Task<int> AddFuelServiceAsync(FuelService fuelService);
        Task UpdateFuelServiceAsync(FuelService fuelService);
        Task DeleteFuelServiceAsync(FuelService fuelService);
        Task<FuelService> GetFuelServiceAsync(int id);
        Task<List<FuelService>> GetFuelServicesForCarAsync(int carId);
    }
}
