using MarketPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.ViewModels
{
    public class VehicleModelVM
    {
        public int Id { get; set; }

        public KeyValuePairVM Vehicle { get; set; }
        public KeyValuePairVM Manufacturer { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdate { get; set; }

        public ContanctVM Contact { get; set; }

        public ICollection<KeyValuePairVM> Features { get; set; }

        public VehicleModelVM()
        {
            Features = new Collection<KeyValuePairVM>();
        }
    }
}
