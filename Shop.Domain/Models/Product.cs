using System.Collections.Generic;

namespace Shop.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Stock> Stock { get; set; }

    }
}
