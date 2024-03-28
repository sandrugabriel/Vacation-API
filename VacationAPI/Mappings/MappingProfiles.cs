using AutoMapper;
using VacationAPI.Models;
using VacationAPI.Dto;

namespace VacationAPI.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {


            CreateMap<CreateRequest, Vacation>();
        }
    }
}
