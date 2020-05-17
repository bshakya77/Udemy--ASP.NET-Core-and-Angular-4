using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.ViewModels
{
    public class ManufacturerVM: KeyValuePairVM
    {
        public ICollection<KeyValuePairVM> Vehicles { get; set; }
        public ManufacturerVM()
        {
            Vehicles = new Collection<KeyValuePairVM>();
        }
    }
}
