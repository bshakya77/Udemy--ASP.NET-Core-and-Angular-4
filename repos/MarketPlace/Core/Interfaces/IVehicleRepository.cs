using MarketPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Interfaces
{
    public interface IVehicleRepository
    {
        Task<QueryResult<VehicleModel>> GetVehicles(VehicleQuery filter);
        
        Task<VehicleModel> GetVehicle(int id, bool includeRelated = true);

        void AddVehicle(VehicleModel vehicleModel);

        void RemoveVehicle(VehicleModel vehicleModel);
    }
}
