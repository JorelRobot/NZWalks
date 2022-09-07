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

        public AuthController(IUserRepository userRespository)
        {
            this.userRespository = userRespository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            // Validate the incoming request

            // Check if user is authenticated

            // Check username and password
            var isAuthenticated = await userRespository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (isAuthenticated)
            {
                // Generate a JWT Token
            }

            return BadRequest();
        }
    }
}
