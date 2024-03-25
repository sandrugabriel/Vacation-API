using VacationAPI.Models;

namespace VacationAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Vacation>> GetAllAsync();

        Task<Vacation> GetByNameAsync(string destination);

        Task<Vacation> GetByIdAsync(int id);
    }
}
