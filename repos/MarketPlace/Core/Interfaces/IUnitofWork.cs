using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Interfaces
{
    public interface IUnitofWork
    {
        Task CompleteAsync();
    }
}
