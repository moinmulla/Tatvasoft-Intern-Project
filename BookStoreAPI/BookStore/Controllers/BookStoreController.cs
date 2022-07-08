using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/BookStore")]
    public class BookStoreController : ControllerBase
    {
        UserRepository _repository=new UserRepository();
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel model)  //Using [FormBody] we can pass parameter as JSON
        {
            User user = _repository.Login(model);
            if (user == null)
                return NotFound("Invalid username or password");

            return Ok(user);
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            User user = _repository.Register(model);
            if (user == null)
                return BadRequest();

            return Ok(user);
        }

    }
}
