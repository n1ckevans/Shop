using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products

{
    public class GetProducts
    {
        private ApplicationDbContext _ctx;

        public GetProducts(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ProductViewModel> Do() =>
            _ctx.Products
                .Include(x => x.Stock)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = $"${x.Price.ToString("N2")}", //110.50 => 1,100.50 => $1,100.50

                    StockCount = x.Stock.Sum(y => y.Quantity)
                })
                .ToList();



        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int StockCount { get; set; }
        }
    }

}
