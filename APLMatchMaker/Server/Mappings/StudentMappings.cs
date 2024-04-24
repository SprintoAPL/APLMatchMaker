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
                dest => dest.StartDate,
                from => from.MapFrom<InternshipStartDateMapping>())
                .ForMember(
                dest => dest.EndDate,
                from => from.MapFrom<InternshipEndDateMapping>());



            CreateMap<Company, StudentWorkAtCompanyShortDTO>();

        }
    }

    public class InternshipStartDateMapping : IValueResolver<Internship, StudentInternshipShortListDTO, DateTime?>
    {
        public DateTime? Resolve(Internship source, StudentInternshipShortListDTO destination, DateTime? destMember, ResolutionContext context)
        {
            return source.AlternateStartDate != null ? source.AlternateStartDate : source.Project!.DefaultStartDate;
        }
    }
    public class InternshipEndDateMapping : IValueResolver<Internship, StudentInternshipShortListDTO, DateTime?>
    {
        public DateTime? Resolve(Internship source, StudentInternshipShortListDTO destination, DateTime? destMember, ResolutionContext context)
        {
            return source.AlternateEndDate != null ? source.AlternateEndDate : source.Project!.DefaultEndDate;
        }
    }
}
