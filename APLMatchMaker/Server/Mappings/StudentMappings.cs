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
                from => from.MapFrom(au => au.Course!.LastOrDefault()!.Course))
                .ForMember(
                dest => dest.Interships,
                from => from.MapFrom(au => au.Internships!))
                .ForMember(
                dest => dest.WorkAtCompany,
                from => from.MapFrom(au => au.Company!));


            CreateMap<ApplicationUser, StudentForUpdateDTO>().ReverseMap();


            CreateMap<Course, StudentAtCourseShortDTO>();


            CreateMap<Internship, StudentInternshipShortListDTO>()
                .ForMember(
                dest => dest.CompanyName,
                from => from.MapFrom(i => i.Project!.Company!.CompanyName))
                .ForMember(
                dest => dest.CompanyId,
                from => from.MapFrom(i => i.Project!.CompanyId))
                .ForMember(
                dest => dest.ProjectDescription,
                from => from.MapFrom(i => i.Project!.ProjectDescription))
                .ForMember(
                dest => dest.ProjectId,
                from => from.MapFrom(i => i.ProjectId))
                .ForMember(
                dest => dest.EndDate,
                from => from.MapFrom(i => i.AlternateStartDate))
                .ForMember(
                dest => dest.StartDate,
                from => from.MapFrom(i => i.AlternateEndDate));



            CreateMap<Company, StudentWorkAtCompanyShortDTO>();

        }
    }
}
