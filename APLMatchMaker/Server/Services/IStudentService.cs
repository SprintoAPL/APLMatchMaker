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
        Task<StudentForDetailsDTO?> GetAsync(Guid id);
        Task<StudentForUpdateDTO?> GetForUpdateAsync(Guid id);
        Task<StudentForDetailsDTO> PostAsync(StudentForCreateDTO dto);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> HasEngagementsAsync(Guid id);
        Task<bool> EmailExistAsync(string email);
        Task<StudentForDetailsDTO?> PatchStudentAsync(Guid id, StudentForUpdateDTO dto);
        Task<StudentForDetailsDTO?> UpdateStudentAsync(Guid id, StudentForUpdateDTO dto);
    }
}