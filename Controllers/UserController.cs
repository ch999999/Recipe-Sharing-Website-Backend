using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RecipeSiteBackend.Auth;
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
        private readonly IJWTManager _jwtManager;

        public UserController(UserService service, IConfiguration config, IJWTManager jwtManager)
        {
            _service = service;
            _configuration = config;
            _jwtManager = jwtManager;
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

            //Code for extracting claims
            //string token = Request.Headers["Authorization"];

            //if (token.StartsWith("Bearer"))
            //{
            //    token = token.Substring("Bearer ".Length).Trim();
            //}
            //var handler = new JwtSecurityTokenHandler();

            //JwtSecurityToken jwt = handler.ReadJwtToken(token);

            //var claims = new Dictionary<string, string>();

            //foreach (var claim in jwt.Claims)
            //{
            //    claims.Add(claim.Type, claim.Value);
            //}
            //return Ok(claims);

            //Test if token is valid only
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

        //[HttpPost]
        //[Route("/api/user/authenticate")]
        //public async Task<IActionResult> Authenticate(LoginUser loginUser)
        //{
        //    var validUser = await _service.IsValidUser(loginUser.Identifier, loginUser.Password);
        //    if (validUser == null)
        //    {
        //        return Unauthorized("Invalid username ot password");

        //    }
        //    var token = _jwtManager.GenerateToken(loginUser.Identifier);
        //    if (token == null)
        //    {
        //        return Unauthorized("Invalid Attempt");

        //    }
        //    UserRefreshToken obj = new UserRefreshToken
        //    {
        //        RefreshToken = token.RefreshToken,
        //        UserName = loginUser.Identifier
        //    };

        //    _service.AddUserRefreshTokens(obj);
        //    return Ok(token);
        //}

        [HttpPost]
        [Route("/api/user/auth-refresh")]
        public async Task<IActionResult> Refresh(Tokens token)
        {
            var principal = _jwtManager.GetPrincipalFromExpiredToken(token.AccessToken);
            var username = principal.Identity?.Name;
            var user = await _service.GetByUsername(username);
            if (user == null)
            {
                return Unauthorized(new {error="invalid user"}); //force user to relog
            }
            var savedRefreshToken = _service.GetSavedRefreshTokens(user.UUID, token.RefreshToken);
            if (savedRefreshToken == null)
            {
                return Unauthorized(new {error="invalid token"}); //force user to relog
            }
            var newJwtToken = _jwtManager.GenerateToken(username, user.Firstname);
            if (newJwtToken == null)
            {
                return Unauthorized(new {error="invalid token"}); //force user to relog
            }
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserUUID = user.UUID,
            };
            _service.DeleteUserRefreshTokens(user.UUID, token.RefreshToken);
            _service.AddUserRefreshTokens(obj);
            return Ok(newJwtToken);
        }

        [HttpPost]
        [Route("/api/User/logout")]
        public async Task<IActionResult> Logout(Tokens token)
        {
            var principal = _jwtManager.GetPrincipalFromExpiredToken(token.AccessToken);
            var username = principal.Identity?.Name;
            var user = await _service.GetByUsername(username);
            if (user == null)
            {
                return Unauthorized(new { error = "invalid user" }); //force user to relog
            }
            _service.DeleteUserRefreshTokens(user.UUID, token.RefreshToken);
            _service.SaveChanges();
            return Ok(new {message= "Logout successful" });
        }

        [HttpPost]
        [Route("/api/User/login2")]
        public async Task<IActionResult> Login2(LoginUser loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Identifier))
            {
                return BadRequest(new ValidationError
                {
                    ErrorField = "identifier",
                    Message = "Username/email cannot be empty"
                });
            }else if (string.IsNullOrEmpty(loginUser.Password))
            {
                return BadRequest(new ValidationError
                {
                    ErrorField = "password",
                    Message = "Password cannot be empty"
                });
            }

            User loggedInUser = await _service.IsValidUser(loginUser.Identifier, loginUser.Password);
            if (loggedInUser == null)
            {
                return BadRequest(new { ErrorField = "password", Message = "Invalid Username/Email or Password." });
            }

            var token = _jwtManager.GenerateToken(loggedInUser.Username, loggedInUser.Firstname);
            if (token == null)
            {
                return Unauthorized(new { ErrorField = "password", Message = "Unknown error occurred. Try again." });
            }
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = token.RefreshToken,
                UserUUID = loggedInUser.UUID
            };

            _service.AddUserRefreshTokens(obj);
            return Ok(token);

        }


    }
}

