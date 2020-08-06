using System.Collections.Generic;
using System.Threading.Tasks;
using Ymmv.Models;

namespace Ymmv.Services
{
    public interface IFuelServiceStore
    {
        Task<int> AddFuelServiceAsync(FuelService fuelService);
        //Task<bool> UpdateItemAsync(T item);
        //Task<bool> DeleteItemAsync(string id);
        Task<FuelService> GetFuelServiceAsync(int id);
        Task<List<FuelService>> GetFuelServicesForCarAsync(int carId);
    }
}
