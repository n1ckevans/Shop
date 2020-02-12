using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Shop.Application.Cart
{
        public class StockManager : IStockManager
        {
            private ApplicationDbContext _ctx;

            public StockManager(ApplicationDbContext ctx)
            {
                _ctx = ctx;
            }

            public bool EnoughStock(int stockId, int quantity)
            {
                
                return _ctx.Stock.FirstOrDefault(x => x.Id == stockId).Quantity >= quantity;
            }

            public Stock GetStockWithProduct(int stockId)
            {
                return _ctx.Stock
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == stockId);
            }

            public Task PutSockOnHold(int stockId, int quantity, string sessionId)
            {
                //database responsibility to update the stock
                _ctx.Stock.FirstOrDefault(x => x.Id == stockId).Quantity -= quantity;

                var stockOnHold = _ctx.StockOnHold
                    .Where(x => x.SessionId == sessionId)
                    .ToList();

                if (stockOnHold.Any(x => x.StockId == stockId))
                {
                    stockOnHold.Find(x => x.StockId == stockId).Quantity += quantity;
                }
                else
                {
                    _ctx.StockOnHold.Add(new StockOnHold
                    {
                        StockId = stockId,
                        SessionId = sessionId,
                        Quantity = quantity,
                        Expiration = DateTime.Now.AddMinutes(20)
                    });
                }

                foreach (var stock in stockOnHold)
                {
                    stock.Expiration = DateTime.Now.AddMinutes(20);
                }

                return _ctx.SaveChangesAsync();
            }

            public Task RemoveStockFromHold(int stockId, int quantity, string sessionId)
            {
            var stockOnHold = _ctx.StockOnHold
                   .FirstOrDefault(x => x.StockId == stockId
                   && x.SessionId == sessionId);

                var stock = _ctx.Stock.FirstOrDefault(x => x.Id == stockId);

                stock.Quantity += quantity;

                stockOnHold.Quantity -= quantity;

                if (stockOnHold.Quantity <= 0)
                {
                    _ctx.Remove(stockOnHold);
                }

                return _ctx.SaveChangesAsync();

            }
        }
}
