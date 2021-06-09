using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserToRegisterDto userToRegisterDTO)
        {
            try
            {
                return Ok(await _authService.RegisterUserAsync(userToRegisterDTO));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(UserToLoginDto userToLoginDTO)
        {
            try
            {
                return Ok(await _authService.LoginUserAsync(userToLoginDTO));
            }
            catch(Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}