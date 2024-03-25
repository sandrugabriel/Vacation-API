using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VacationAPI.Models;
using VacationAPI.Repository.interfaces;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/v1/vacation")]
    public class ControllerVacation : ControllerBase
    {

        private readonly ILogger<ControllerVacation> _logger;

        private IRepository _repository;

        public ControllerVacation(ILogger<ControllerVacation> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Vacation>>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }


        [HttpGet("/findById")]
        public async Task<ActionResult<Vacation>> GetById([FromQuery] int id)
        {
            var car = await _repository.GetByIdAsync(id);
            return Ok(car);
        }


        [HttpGet("/find/{destination}")]
        public async Task<ActionResult<Vacation>> GetByNameRoute([FromRoute] string destination)
        {
            var car = await _repository.GetByNameAsync(destination);
            return Ok(car);
        }

    }
}
