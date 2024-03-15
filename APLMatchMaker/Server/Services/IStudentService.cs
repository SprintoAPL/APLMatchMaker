using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using APLMatchMaker.Server.ResourceParameters;

namespace APLMatchMaker.Server.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentForListDTO>> GetAsync();
        Task<IEnumerable<StudentForListDTO>> GetAsync(StudentResourceParameters? studentResourceParameters);
        Task<StudentForDetailsDTO?> GetAsync(string id);
        Task<StudentForDetailsDTO> PostAsync(StudentForCreateDTO dto);
        Task<bool> RemoveAsync(string id);
    }
}