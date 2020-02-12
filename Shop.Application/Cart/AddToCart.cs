using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public AddToCart(
            ISessionManager sessionManager,
            IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            //service responsibility 
            if (!_stockManager.EnoughStock(request.StockId, request.Quantity))
            {
                return false;
            }

            await _stockManager
                .PutSockOnHold(request.StockId, request.Quantity, _sessionManager.GetId());

            var stock = _stockManager.GetStockWithProduct(request.StockId);

            var cartProduct = new CartProduct()
            {
                ProductId = stock.Product.Id,
                ProductName = stock.Product.Name,
                StockId = stock.Id,
                Quantity = request.Quantity,            
                Price = stock.Product.Price
            };
            
            _sessionManager.AddProduct(cartProduct);

            return true;
        }

    }
}
