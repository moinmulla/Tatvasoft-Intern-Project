using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        UserRepository _repository=new UserRepository();
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUers()
        {
            return Ok(_repository.GetUsers());
        }

        
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUer(int id)
        {
            var user=_repository.GetUser(id);
            if(user==null)
                return NotFound();

            return Ok(user);

        }

        [HttpGet]
        [Route("Roles")]
        public IActionResult GetRoles()
        {
            var user = _repository.GetRoles();
            if (user == null)
                return NotFound();

            return Ok(user);

        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            User user = _repository.Register(model);
            if (user == null)
                return BadRequest();

            return Ok(user);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateUser(UserModel model)
        {
            if (model != null)
            {
                var user = _repository.GetUser(model.id);
                if (user == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                }
                user.Firstname=model.Firstname;
                user.Lastname=model.Lastname;
                user.Email=model.Email;

                var saved = _repository.UpdateUser(user);
                if (saved)
                {
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), "User details updated successfully");
                }
            }

            return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please prvide proper information");
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id > 0)
            {
                var user=_repository.GetUser(id);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");
                var isDeleted = _repository.DeleteUser(user);
                if (isDeleted)
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail successfully deleted");
            }
            return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide proper information");
        }

    }
}
