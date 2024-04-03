using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VacationAPI.Controllers.interfaces;
using VacationAPI.Dto;
using VacationAPI.Exceptions;
using VacationAPI.Models;
using VacationAPI.Repository.interfaces;
using VacationAPI.Service.interfaces;

namespace VacationAPI.Controllers
{

    public class ControllerVacation : ControllerAPI
    {


        private IQueryService _queryService;
        private ICommandService _commandService;

        public ControllerVacation(IQueryService queryService, ICommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Vacation>>> GetAll()
        {
            try
            {
                var vacations = await _queryService.GetAll();

                return Ok(vacations);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Vacation>> GetByName([FromQuery] string name)
        {

            try
            {
                var vacation = await _queryService.GetByNameAsync(name);
                return Ok(vacation);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Vacation>> GetById([FromQuery] int id)
        {

            try
            {
                var vacation = await _queryService.GetById(id);
                return Ok(vacation);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Vacation>> CreateVacation(CreateRequest request)
        {
            try
            {
                var vacation = await _commandService.Create(request);
                return Ok(vacation);
            }
            catch (InvaidPrice ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Vacation>> UpdateVacation([FromQuery] int id, UpdateRequest request)
        {
            try
            {
                var vacation = await _commandService.Update(id, request);
                return Ok(vacation);
            }
            catch (InvaidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Vacation>> DeleteVacation([FromQuery] int id)
        {
            try
            {
                var vacation = await _commandService.Delete(id);
                return Ok(vacation);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
