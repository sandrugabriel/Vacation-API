using System.ComponentModel.Design;
using VacationAPI.Dto;
using VacationAPI.Exceptions;
using VacationAPI.Models;
using VacationAPI.Repository.interfaces;
using VacationAPI.Service.interfaces;

namespace VacationAPI.Service
{
    public class CommandService : ICommandService
    {


        private IRepository _repository;

        public CommandService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Vacation> Create(CreateRequest request)
        {

            if (request.Price <= 0)
            {
                throw new InvaidPrice(Constants.Constants.InvalidPrice);
            }

            var vacation = await _repository.Create(request);

            return vacation;
        }

        public async Task<Vacation> Update(int id, UpdateRequest request)
        {

            var vacation = await _repository.GetByIdAsync(id);
            if (vacation == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (request.Price <= 0)
            {
                throw new InvaidPrice(Constants.Constants.InvalidPrice);
            }
            vacation = await _repository.Update(id, request);
            return vacation;
        }

        public async Task<Vacation> Delete(int id)
        {

            var vacation = await _repository.GetByIdAsync(id);
            if (vacation == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);
            return vacation;
        }
    }
}
