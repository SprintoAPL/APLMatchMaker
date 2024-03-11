using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Server.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentForListDTO>> GetAsync();
        Task<StudentForDetailsDTO?> GetAsync(string id);
        Task<StudentForDetailsDTO> PostAsync(StudentForCreateDTO dto);
        Task<bool> RemoveAsync(string id);
    }
}