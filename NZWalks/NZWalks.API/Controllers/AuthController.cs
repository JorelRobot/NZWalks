using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRespository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRespository, ITokenHandler tokenHandler)
        {
            this.userRespository = userRespository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            // Validate the incoming request

            // Check if user is authenticated

            // Check username and password
            var user = await userRespository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                // Generate a JWT Token
                var token = tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest();
        }
    }
}
