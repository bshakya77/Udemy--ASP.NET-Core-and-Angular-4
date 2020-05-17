using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.ViewModels
{
    public class SaveVehicleVM
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public bool IsRegistered { get; set; }

        public ContanctVM Contact { get; set; }

        public ICollection<int> Features { get; set; }

        public SaveVehicleVM()
        {
            Features = new Collection<int>();
        }

    }
}
