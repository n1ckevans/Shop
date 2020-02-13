using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Products

{
    [Service]
    public class GetProducts
    {
        private IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do() =>
           _productManager.GetProductsWithStock(x => new ProductViewModel
           {
               Name = x.Name,
               Description = x.Description,
               PhotoUrl = x.PhotoUrl,
               Price = x.Price.GetPriceString(),
               StockCount = x.Stock.Sum(y => y.Quantity)
           });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public string PhotoUrl { get; set; }
            public int StockCount { get; set; }
        }
    }

}
