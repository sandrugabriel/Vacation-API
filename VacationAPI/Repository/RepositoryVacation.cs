using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VacationAPI.Data;
using VacationAPI.Dto;
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


        public async Task<Vacation> Create(CreateRequest request)
        {

            var vacantion = _mapper.Map<Vacation>(request);

            _context.Vacations.Add(vacantion);

            await _context.SaveChangesAsync();

            return vacantion;
        }

        public async Task<Vacation> Update(int id, UpdateRequest request)
        {

            var vacantion = await _context.Vacations.FindAsync(id);

            vacantion.Destination = request.Destination ?? vacantion.Destination;
            vacantion.duration = request.Duration ?? vacantion.duration;
            vacantion.Price = request.Price ?? vacantion.Price;

            _context.Vacations.Update(vacantion);

            await _context.SaveChangesAsync();

            return vacantion;

        }

        public async Task<Vacation> DeleteById(int id)
        {
            var vacantion = await _context.Vacations.FindAsync(id);

            _context.Vacations.Remove(vacantion);

            await _context.SaveChangesAsync();

            return vacantion;
        }
    }
}
