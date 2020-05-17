using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Models;
using MarketPlace.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MarketPlace.Persistence.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly MarketPlaceDbContext context;
        public VehicleRepository(MarketPlaceDbContext context)
        {
            this.context = context;
        }

        public void AddVehicle(VehicleModel vehicleModel)
        {
            context.VehicleModels.Add(vehicleModel);
        }

        public async Task<VehicleModel> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.VehicleModels.FindAsync(id);

            return await context.VehicleModels
                .Include(v => v.Features)
                  .ThenInclude(vf => vf.Feature)
                .Include(v => v.Vehicle)
                  .ThenInclude(m => m.Manufacturer)
                .SingleOrDefaultAsync(v => v.Id == id);

        }

        public async Task<QueryResult<VehicleModel>> GetVehicles(VehicleQuery vehicleQuery)
        {
            var result = new QueryResult<VehicleModel>();

            var query = context.VehicleModels
                     .Include(v => v.Vehicle)
                       .ThenInclude(m => m.Manufacturer)
                     .AsQueryable();

            query = query.ApplyFiltering(vehicleQuery);

            var columnsMap = new Dictionary<string, Expression<Func<VehicleModel, object>>>()
            {
                ["manufacturer"] = v => v.Vehicle.Manufacturer.Name,
                 ["vehicle"] = v => v.Vehicle.Name,
                 ["contactName"] = v => v.ContactName
            };

            query = query.ApplyOrdering(vehicleQuery, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(vehicleQuery);

            result.Items = await query.ToListAsync();

            return result;
        }

        public void RemoveVehicle(VehicleModel vehicleModel)
        {
            context.Remove(vehicleModel);
        }


    }
}
