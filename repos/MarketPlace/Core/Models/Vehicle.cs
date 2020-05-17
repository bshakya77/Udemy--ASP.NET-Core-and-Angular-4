using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public int ManufacturerId { get; set; }
    }
}
