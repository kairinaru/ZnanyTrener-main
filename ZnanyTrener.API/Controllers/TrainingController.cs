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
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _service;
        public TrainingController(ITrainingService service)
        {
            _service = service;
        }

        [Authorize(Policy = "RequireUserRole")]
        [HttpPost]
        public async Task<IActionResult> AddTraining(TrainingToAddDto trainingToAddDto) 
        {
            try
            {
                return Ok(await _service.AddAsync(trainingToAddDto));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireCoachRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(int id) 
        {
            try
            {
                return Ok(await _service.DeleteAsync(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireLogged")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTraining(int id) 
        {
            try
            {
                return Ok(await _service.GetAsync(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireLogged")]
        [HttpGet("coach/{id}")]
        public async Task<IActionResult> GetTrainingsForCoach(int id) 
        {
            try
            {
                return Ok(await _service.GetForCoachAsync(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }   

        [Authorize(Policy = "RequireUserRole")]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetTrainingsForUser(int id) 
        {
            try
            {
                return Ok(await _service.GetForUserAsync(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}