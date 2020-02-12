using Shop.Application.Infrastructure;
using Shop.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _ctx;

        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; }
        }

        public async Task<bool> Do(Request request)
        {

            var stockOnHold = _ctx.StockOnHold
                   .FirstOrDefault(x => x.StockId == request.StockId
                   && x.SessionId == _sessionManager.GetId());

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == request.StockId);

            if (request.All)
            {
                stock.Quantity += stockOnHold.Quantity;
                _sessionManager.RemoveProduct(request.StockId, stockOnHold.Quantity);
                stockOnHold.Quantity = 0;
            }
            else
            {
                stock.Quantity += request.Quantity;
                stockOnHold.Quantity -= request.Quantity;
                _sessionManager.RemoveProduct(request.StockId, request.Quantity);
            }

            if(stockOnHold.Quantity <= 0)
            {
                _ctx.Remove(stockOnHold);
            }

            await _ctx.SaveChangesAsync();

            return true;
        }

    }
}
