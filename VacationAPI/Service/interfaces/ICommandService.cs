using VacationAPI.Dto;
using VacationAPI.Models;

namespace VacationAPI.Service.interfaces
{
    public interface ICommandService
    {
        Task<Vacation> Create(CreateRequest request);

        Task<Vacation> Update(int id, UpdateRequest request);

        Task<Vacation> Delete(int id);
    }
}
