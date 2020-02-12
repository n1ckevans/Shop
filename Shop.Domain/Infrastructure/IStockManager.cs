using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
        public interface IStockManager
        {
            Stock GetStockWithProduct(int stockId);
            bool EnoughStock(int stockId, int quantity);
            Task PutSockOnHold(int stockId, int quantity, string sessionId);
        Task RemoveStockFromHold(int stockId, int quantity, string sessionId);
    }
}
