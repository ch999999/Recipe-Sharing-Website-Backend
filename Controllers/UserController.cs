using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RecipeSiteBackend.Models;
using RecipeSiteBackend.Services;
using RecipeSiteBackend.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RecipeSiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _configuration;

        public UserController(UserService service, IConfiguration config)
        {
            _service = service;
            _configuration = config;
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
                    ErrorField = "identifier",
                    Message = "Username/email cannot be empty"
                });
            }

            User loggedInUser = await _service.Login(loginUser.Identifier, loginUser.Password);
            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }

            return BadRequest(new { ErrorField = "password", Message = "Invalid Username/Email or Password." } );
        }

        [EnableCors]
        [Authorize]
        [HttpPost]
        [Route("/api/User/authcheck")]
        public async Task<IActionResult> ValidateToken()
        {
            //code for jwt authentication (not necessary due to [Authorize] performing the same thing)

            //string token = Request.Headers["Authorization"];
            //if (token.StartsWith("Bearer"))
            //{
            //    token = token.Substring("Bearer ".Length).Trim();
            //}

            /////////
            //var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            //var signingkey = new SymmetricSecurityKey(key);
            //var signingkeys = new List<SecurityKey> { signingkey };
            //var validationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = true,
            //    ValidIssuer = _configuration["JWT:Issuer"],
            //    ValidateAudience = true,
            //    ValidAudience = _configuration["JWT:Audience"],
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKeys = signingkeys,
            //    ValidateLifetime = true
            //};

            //try
            //{
            //    var handler = new JwtSecurityTokenHandler();
            //    handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            //    return Ok();
            //}catch(SecurityTokenValidationException ex)
            //{
            //    return Unauthorized(new { Error = "Invalid or expired access token." });
            //}
            return Ok();
            
            
        }

        [EnableCors]
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
                    ErrorField = "username",
                    Message = "Username already taken"
                });
            }

            if (await _service.GetByEmail(newUser.Email) != null)
            {
                return Conflict(new ValidationError
                {
                    ErrorField = "email",
                    Message = "Email already taken"
                });
            }

            newUser.Role = "Normal";
            var user = await _service.CreateUser(newUser);

            return Ok(user);
            
        }
    }
}

