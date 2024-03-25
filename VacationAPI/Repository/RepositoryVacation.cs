using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VacationAPI.Data;
using VacationAPI.Models;
using VacationAPI.Repository.interfaces;

namespace VacationAPI.Repository
{
    public class RepositoryVacation : IRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryVacation(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Vacation>> GetAllAsync()
        {
            return await _context.Vacations.ToListAsync();
        }

        public async Task<Vacation> GetByIdAsync(int id)
        {
            List<Vacation> cars = await _context.Vacations.ToListAsync();

            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Id == id) return cars[i];
            }

            return null;
        }

        public async Task<Vacation> GetByNameAsync(string destination)
        {
            List<Vacation> allcars = await _context.Vacations.ToListAsync();

            for (int i = 0; i < allcars.Count; i++)
            {
                if (allcars[i].Destination.Equals(destination))
                {
                    return allcars[i];
                }
            }

            return null;
        }

    }
}
