using Shop.Domain.Infrastructure;
using System.Linq;

namespace Shop.Application.ProductsAdmin
{
    [Service]
    public class GetProduct
    {
        private IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public ProductViewModel Do(int id) =>
           _productManager.GetProductById(id, x => new ProductViewModel
           {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Price = x.Price,
           });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}