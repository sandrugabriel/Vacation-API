using VacationAPI.Models;

namespace VacationAPI.Service.interfaces
{
    public interface IQueryService
    {
        Task<List<Vacation>> GetAll();

        Task<Vacation> GetById(int id);

        Task<Vacation> GetByNameAsync(string name);
    }
}
