using VacationAPI.Dto;
using VacationAPI.Models;

namespace VacationAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Vacation>> GetAllAsync();

        Task<Vacation> GetByNameAsync(string destination);

        Task<Vacation> GetByIdAsync(int id);


        Task<Vacation> Create(CreateRequest request);

        Task<Vacation> Update(int id, UpdateRequest request);

        Task<Vacation> DeleteById(int id);
    }
}
