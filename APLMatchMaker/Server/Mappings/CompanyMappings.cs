using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using AutoMapper;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Server.Mappings
{
    public class CompanyMappings : Profile
    {
        public CompanyMappings()
        {
            CreateMap<Company, CompanyForListDTO>();

            CreateMap<Company, CompanyForCreateDTO>().ReverseMap();

            CreateMap<CompanyUpdateDTO, Company>().ReverseMap();

            CreateMap<Company, CompanyDetailsDTO>();

            CreateMap<ApplicationUser, CompanyEmployeeShortListDTO>()
                .ForMember(
                    dest => dest.FullName,
                    from => from.MapFrom(au => $"{au.FirstName} {au.LastName}"))
                .ForAllMembers(au => au.Condition(au => au.IsStudent));

            CreateMap<ApplicationUser, CompanyContactsShortListDTO>()
                .ForMember(
                    dest => dest.FullName,
                    from => from.MapFrom(au => $"{au.FirstName} {au.LastName}"))
                .ForAllMembers(au => au.Condition(au => au.IsCompanyContact));

            CreateMap<Internship, CompanyInternshipsShortListDTO>();

            //CreateMap<Internship, CompanyInternshipsShortListDTO>()
            //    .ForMember(
            //        dest => dest.ProjectName,
            //        from => from.MapFrom(i => i.Project!.ProjectName))
            //    .ForMember(
            //        dest => dest.FullName,
            //        from => from.MapFrom(i => $"{i.Student!.FirstName} {i.Student.LastName}"))
            //    .ForMember(
            //        dest => dest.UserId,
            //        from => from.MapFrom(i => i.ApplicationUserId))
            //    .ForMember(
            //        dest => dest.StartDate,
            //        from => from.MapFrom<CompanyInternshipStartDateMapping>())
            //    .ForMember(
            //        dest => dest.EndDate,
            //        from => from.MapFrom<CompanyInternshipEndDateMapping>());
        }
    }

    public class CompanyInternshipStartDateMapping : IValueResolver<Internship, CompanyInternshipsShortListDTO, DateTime?>
    {
        public DateTime? Resolve(Internship source, CompanyInternshipsShortListDTO destination, DateTime? destMember, ResolutionContext context)
        {
            return source.AlternateStartDate != null ? source.AlternateStartDate : source.Project!.DefaultStartDate;
        }
    }
    public class CompanyInternshipEndDateMapping : IValueResolver<Internship, CompanyInternshipsShortListDTO, DateTime?>
    {
        public DateTime? Resolve(Internship source, CompanyInternshipsShortListDTO destination, DateTime? destMember, ResolutionContext context)
        {
            return source.AlternateEndDate != null ? source.AlternateEndDate : source.Project!.DefaultEndDate;
        }
    }
}
