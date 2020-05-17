using MarketPlace.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models
{
    public class VehicleQuery: IQueryObject
    {
        public int? ManufacturerId { get; set; }
        public int? VehicleId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
