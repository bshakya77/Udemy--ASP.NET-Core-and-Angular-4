using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models
{
    [Table("Manufacturers")]
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public Manufacturer()
        {
            Vehicles = new Collection<Vehicle>();
        }
    }
}
