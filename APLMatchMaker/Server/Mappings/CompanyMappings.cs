using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using AutoMapper;

namespace APLMatchMaker.Server.Mappings
{
    public class CompanyMappings : Profile
    {
        public CompanyMappings()
        {
            CreateMap<Company, CompanyForListDTO>();

            CreateMap<Company, CompanyForCreateDTO>().ReverseMap();

            CreateMap<CompanyUpdateDTO, Company>().ReverseMap();

            CreateMap<Company, CompanyDetailsDTO>()
                .ForMember(
                    dest => dest.Contacts,
                    from => from.MapFrom(co => co.CompanyContacts))
                .ForMember(
                    dest => dest.EmployedStudents,
                    from => from.MapFrom(co => co.CompanyContacts))
                .ForMember(
                    dest => dest.Internships,
                    from => from.MapFrom<CompanyInternships>());

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

    public class CompanyInternships : IValueResolver<Company, CompanyDetailsDTO, ICollection<CompanyInternshipsShortListDTO>?>
    {
        public ICollection<CompanyInternshipsShortListDTO>? Resolve(Company source, CompanyDetailsDTO destination, ICollection<CompanyInternshipsShortListDTO>? destMember, ResolutionContext context)
        {
            ICollection<CompanyInternshipsShortListDTO> result = new List<CompanyInternshipsShortListDTO>(0);

            foreach (var project in source.Projects!) 
            {
                foreach (var intern in project.Internships!)
                {
                    if (intern != null)
                    {
                        result.Add(new CompanyInternshipsShortListDTO { 
                        ApplicationUserId = intern.ApplicationUserId,
                        ProjectId = intern.ProjectId,
                        AlternateStartDate = intern.AlternateStartDate,
                        AlternateEndDate = intern.AlternateEndDate});
                    }
                }
            }
            return result;
        }
    }
}
