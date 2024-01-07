using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Services;
using RecipeSiteBackend.Models;
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

        [HttpPost]
        public IActionResult CreateUser(User newUser)
        {
            try
            {
                var user = _service.CreateUser(newUser);
                return Ok(user);
            }catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.StartsWith("23505"))
                    {
                        return Conflict("Username or Email already exists");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            
            
            
        }
    }
}
