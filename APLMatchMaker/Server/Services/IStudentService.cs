using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Server.Helpers;
using Microsoft.AspNetCore.JsonPatch;

namespace APLMatchMaker.Server.Services
{
    public interface IStudentService
    {
        //Task<IEnumerable<StudentForListDTO>> GetAsync();
        Task<(IEnumerable<StudentForListDTO>, PagingFactoids)> GetAsync(StudentResourceParameters? studentResourceParameters);
        Task<StudentForDetailsDTO?> GetAsync(string id);
        Task<StudentForUpdateDTO?> GetForUpdateAsync(string id);
        Task<StudentForDetailsDTO> PostAsync(StudentForCreateDTO dto);
        Task<bool> RemoveAsync(string id);
        Task<bool> EmailExistAsync(string email);
        Task<StudentForDetailsDTO?> UpdateStudentAsync(string id, StudentForUpdateDTO dto);
    }
}