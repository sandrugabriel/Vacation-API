using Microsoft.AspNetCore.Mvc;
using VacationAPI.Dto;
using VacationAPI.Models;

namespace VacationAPI.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPI : ControllerBase
    {


        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Vacation>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Vacation>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Vacation))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Vacation>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Vacation))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Vacation>> GetByName([FromQuery] string name);

        [HttpPost("CreateVacation")]
        [ProducesResponseType(statusCode: 201, type: typeof(Vacation))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Vacation>> CreateVacation(CreateRequest request);

        [HttpPut("UpdateVacation")]
        [ProducesResponseType(statusCode: 200, type: typeof(Vacation))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Vacation>> UpdateVacation([FromQuery] int id, UpdateRequest request);

        [HttpDelete("DeleteVacation")]
        [ProducesResponseType(statusCode: 200, type: typeof(Vacation))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Vacation>> DeleteVacation([FromQuery] int id);
    }
}
