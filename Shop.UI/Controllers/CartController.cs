using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System.Linq;
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

        [HttpPost("{stockId}/{quantity}")]
        public async Task<IActionResult> Remove(
            int stockId,
            int quantity,
            [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = quantity
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Item removed from Cart");
            }
            return BadRequest("Failed to remove Item from Cart");
        }

        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalPrice = getCart.Do().Sum(x => x.RealPrice * x.Quantity);

            return PartialView("Components/Cart/Small", $"${totalPrice}");
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();

            return PartialView("_CartPartial", cart);
        }

    }
}
