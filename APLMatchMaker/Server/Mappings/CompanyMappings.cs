using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using AutoMapper;
using System.Collections.Generic;

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
                    dest => dest.EmployedStudents,
                    from => from.MapFrom<CompanyEmployedStudents>())
                .ForMember(
                    dest => dest.Contacts,
                    from => from.MapFrom<CompanyContacts>())
                .ForMember(
                    dest => dest.Internships,
                    from => from.MapFrom<CompanyInternships>());
        }
    }

    public class CompanyEmployedStudents : IValueResolver<Company?, CompanyDetailsDTO, ICollection<CompanyEmployeeShortListDTO>?>
    {
        public ICollection<CompanyEmployeeShortListDTO>? Resolve(Company? source, CompanyDetailsDTO destination, ICollection<CompanyEmployeeShortListDTO>? destMember, ResolutionContext context)
        {
            ICollection<CompanyEmployeeShortListDTO> result = new List<CompanyEmployeeShortListDTO>(0);

            foreach (var employee in source!.CompanyContacts!)
            {
                if (employee.IsStudent)
                {
                    result.Add( new CompanyEmployeeShortListDTO
                    {
                        Id = employee.Id,
                        FullName = $"{employee.FirstName} {employee.LastName}",
                    });
                }
            }
            return result;
        }
    }

    public class CompanyContacts : IValueResolver<Company, CompanyDetailsDTO, ICollection<CompanyContactsShortListDTO>?>
    {
        public ICollection<CompanyContactsShortListDTO>? Resolve(Company source, CompanyDetailsDTO destination, ICollection<CompanyContactsShortListDTO>? destMember, ResolutionContext context)
        {
            ICollection<CompanyContactsShortListDTO> result = new List<CompanyContactsShortListDTO>(0);

            foreach (var contact in source.CompanyContacts!)
            {
                if (contact.IsCompanyContact)
                {
                    result.Add(new CompanyContactsShortListDTO
                    {
                        Id = contact.Id,
                        FullName = $"{contact.FirstName} {contact.LastName}",
                        Title = contact.Title,
                    });
                }
            }
            return result;
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
                        UserId = intern.ApplicationUserId,
                        FullName = $"{intern.Student!.FirstName} {intern.Student!.LastName}",
                        ProjectId = intern.ProjectId,
                        ProjectName = intern.Project!.ProjectName,
                        StartDate = intern.AlternateStartDate != null ? intern.AlternateStartDate : intern.Project.DefaultStartDate,
                        EndDate = intern.AlternateEndDate != null ? intern.AlternateEndDate : intern.Project.DefaultEndDate,
                        });
                    }
                }
            }
            return result;
        }
    }
}
