using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }
        
        [Authorize(Policy = "RequireUserRole")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync( )
        {
            try 
            {
                return Ok(await _service.CreateSessionAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}