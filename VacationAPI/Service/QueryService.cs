using VacationAPI.Exceptions;
using VacationAPI.Models;
using VacationAPI.Repository.interfaces;
using VacationAPI.Service.interfaces;

namespace VacationAPI.Service
{
    public class QueryService : IQueryService
    {
        private IRepository _repository;

        public QueryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Vacation>> GetAll()
        {
            var vacation = await _repository.GetAllAsync();

            if (vacation.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Vacation>)vacation;
        }

        public async Task<Vacation> GetByNameAsync(string name)
        {
            var vacation = await _repository.GetByNameAsync(name);

            if (vacation == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return vacation;
        }

        public async Task<Vacation> GetById(int id)
        {
            var vacation = await _repository.GetByIdAsync(id);

            if (vacation == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return vacation;
        }
    }
}
