using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) 
        {
            try
            {
                return Ok(await _service.DeleteUser(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireLogged")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserToEditDto userToEdit)
        {
            try
            {
                return Ok(await _service.UpdateUser(userToEdit));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCoaches(string keyWord)
        {
            try
            {
                return Ok(await _service.FilterCoach(keyWord));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                return Ok(await _service.GetUser(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireLogged")]
        [HttpPost]
        public async Task<IActionResult> ChangeAvatar([FromForm] AvatarForChange avatarForChange)
        {
            try
            {
                var myID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                avatarForChange.UserId = myID;
                
                await _service.ChangeAvatar(avatarForChange);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}