using AutoMapper;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Server.Mappings
{
    public class StudentMappings : Profile
    {
        public StudentMappings()
        {
            CreateMap<ApplicationUser, StudentForListDTO>()
                .ForMember(
                dest => dest.Name,
                from => from.MapFrom(au => $"{au.FirstName} {au.LastName}"));


            CreateMap<ApplicationUser, StudentForDetailsDTO>()
                .ForMember(
                dest => dest.Course,
                from => from.MapFrom(au => au.Course.FirstOrDefault()!.Course));


            CreateMap<ApplicationUser, StudentForUpdateDTO>().ReverseMap();


            CreateMap<Course, CourseForStudentDTO>();

        }
    }
}
