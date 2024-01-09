using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Models;
using RecipeSiteBackend.Services;
using RecipeSiteBackend.Validation;
using System.IdentityModel.Tokens.Jwt;

namespace RecipeSiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

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
        [Route("/api/User/login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            
            if (string.IsNullOrEmpty(loginUser.Identifier))
            {
                return BadRequest(new ValidationError
                {
                    ErrorField = "Credentials",
                    Message = "Credentials cannot be empty"
                });
            }

            User loggedInUser = await _service.Login(loginUser.Identifier, loginUser.Password);
            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }

            return BadRequest(new { ErrorField = "Password", Message = "Invalid Credentials or Password." } );
        }

        //[Authorize(Roles = "Everyone")]
        [HttpGet]
        public IActionResult Test()
        {
            string token = Request.Headers["Authorization"];
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();

            }
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            var claims = new Dictionary<string, string>();
            foreach(var claim in jwt.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }
            return Ok(claims);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            //check if user input is valid.
            var validationError = UserValidator.validateInput(newUser);

            if (validationError != null)
            {
                return BadRequest(validationError);
            }

            //check if username and email are taken already or not.
            if (await _service.GetByUsername(newUser.Username) != null)
            {
                return Conflict(new ValidationError
                {
                    ErrorField = "Username",
                    Message = "Username already taken"
                });
            }

            if (await _service.GetByEmail(newUser.Email) != null)
            {
                return Conflict(new ValidationError
                {
                    ErrorField = "Email",
                    Message = "Email already taken"
                });
            }

            newUser.Role = "Normal";
            var user = await _service.CreateUser(newUser);

            return Ok(user);
            
        }
    }
}

