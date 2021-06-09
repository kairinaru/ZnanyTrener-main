using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnanyTrener.API.Dtos;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _service;
        public CertificateController(ICertificateService service)
        {
            _service = service;
        }

        [Authorize(Policy = "RequireCoachRole")]
        [HttpPost]
        public async Task<IActionResult> AddCertificate(CertificateToAddDto certificate) 
        {
            try
            {
                return Ok(await _service.AddCertificate(certificate));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}