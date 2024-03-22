using AutoMapper;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Server.Mappings
{
    public class StudentMappings : Profile
    {
        public StudentMappings()
        {
            CreateMap<ApplicationUser, StudentForListDTO>()
                .ForMember(
                dest => dest.FullName,
                from => from.MapFrom(au => $"{au.FirstName} {au.LastName}"));


            CreateMap<ApplicationUser, StudentForDetailsDTO>()
                .ForMember(
                dest => dest.FullName,
                from => from.MapFrom(au => $"{au.FirstName} {au.LastName}"));


            CreateMap<ApplicationUser, StudentForUpdateDTO>().ReverseMap();

        }
    }
}
