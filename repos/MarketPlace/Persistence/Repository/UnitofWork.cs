
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Core.Interfaces;

namespace MarketPlace.Persistence.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly MarketPlaceDbContext context;

        public UnitofWork(MarketPlaceDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
