using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleModelId { get; set; }

        public int FeatureId { get; set; }

        public VehicleModel VehicleModel { get; set; }

        public Feature Feature { get; set; }
    }
}
