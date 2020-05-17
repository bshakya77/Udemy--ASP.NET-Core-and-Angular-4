using MarketPlace.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Persistence
{
    public class MarketPlaceDbContext: DbContext
    {
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> dbContextOptions)
           : base(dbContextOptions)
        {

        }

        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.VehicleModelId, vf.FeatureId });
        }
    }
}
