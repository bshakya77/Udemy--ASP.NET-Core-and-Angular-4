using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Persistence.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly MarketPlaceDbContext context;

        public PhotoRepository(MarketPlaceDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }
    }
}
