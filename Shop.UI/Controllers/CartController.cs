using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(
            int stockId,
            [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var success = await addToCart.Do(request);

            if(success)
            {
                return Ok("Item Added to Cart");
            }
            return BadRequest("Failed to Add to Cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(
            int stockId,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Item removed from Cart");
            }
            return BadRequest("Failed to remove Item from Cart");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(
            int stockId,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Item removed from Cart");
            }
            return BadRequest("Failed to remove Item from Cart");
        }
    }
}
