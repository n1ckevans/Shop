using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class StockOnHold
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Quantity { get; set; }
        public DateTime Expiration { get; set; }
    }
}
