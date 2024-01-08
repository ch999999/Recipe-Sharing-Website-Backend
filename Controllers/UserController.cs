using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Services;
using RecipeSiteBackend.Models;
using RecipeSiteBackend.Validation;
using System.Data;
using Npgsql;

namespace RecipeSiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        //[HttpGet("{id}")]
        //private ActionResult<User> GetById(int id)
        //{

        //    var user = _service.GetById(id);

        //    if (user is not null)
        //    {
        //        return user;
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //}




        [HttpPost]
        public IActionResult CreateUser(User newUser)
        {
            //check if user input is valid.
            var validationError = UserValidator.validateInput(newUser);

            if (validationError != null)
            {
                return BadRequest(validationError);
            }

            //check if username and email are taken already or not.
            if(_service.GetByUsername(newUser.Username)!=null) 
            {
                return Conflict(new ValidationError
                {
                    ErrorField = "Username",
                    Message = "Username already taken"
                });
            }

            if(_service.GetByEmail(newUser.Email)!=null)
            {
                return Conflict(new ValidationError
                {
                    ErrorField = "Email",
                    Message = "Email already taken"
                });
            }


            try
            {
                var user = _service.CreateUser(newUser);
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }



        }
    }
}

