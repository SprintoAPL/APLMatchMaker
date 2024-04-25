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
        }
    }
}
