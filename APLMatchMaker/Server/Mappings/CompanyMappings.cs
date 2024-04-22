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
            CreateMap<Company,CompanyForCreateDTO>();
            CreateMap<CompanyUpdateDTO, Company>(); 
            CreateMap<Company, CompanyDetailsDTO>(); 
           

        }
    }
}
