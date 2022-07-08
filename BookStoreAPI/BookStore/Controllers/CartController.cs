using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        CartRepository _cartRepository=new CartRepository();

        [HttpGet]
        //[Route("list/{id}")]
        [Route("list/{UserId}")]
        [HttpGet]
        public IActionResult GetCart(int UserId)
        {

            try
            {
                var cart = _cartRepository.GetCartItems(UserId);

                if (cart == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                ListResponse<GetCartModel> listResponse = new ListResponse<GetCartModel>()
                {
                    Results = cart.Results.Select(x => new GetCartModel(x)).ToList(),
                    TotalRecords = cart.TotalRecords
                };
                return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet]
        [Route("list/book/{id}")]
        public IActionResult getCartBook(int id)
        {
            var carts = _cartRepository?.GetCartBook(id);
            return Ok(carts);



        }

        [HttpPost]
        [Route("add")]

        public IActionResult addCart(CartModel model)
        {
            if(model== null)
                return BadRequest("Cart is null");

            Cart cart = new Cart()
            {
                Id = model.Id,
                Userid = model.Userid,
                Bookid = model.Bookid,
                Quantity = model.Quantity,
            };
            cart=_cartRepository.AddCart(cart);
            CartModel cartModel = new CartModel(cart);
            return Ok(cartModel);  

        }

        [HttpPut]
        [Route("Update")]

        public IActionResult updateCart(CartModel model)
        {
            if (model == null)
                return BadRequest("Cart is null");

            Cart cart = new Cart()
            {
                Id = model.Id,
                Userid = model.Userid,
                Bookid = model.Bookid,
                Quantity = model.Quantity
            };
            cart = _cartRepository.UpdateCart(cart);
            CartModel cartModel = new CartModel(cart);
            return Ok(cartModel);

        }

        [HttpDelete]
        [Route("{id}")]
        
        public IActionResult DeleteCart(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id is null");
            }
            var result = _cartRepository.DeleteCart(id);

            return Ok(result);

        }
    }
}
