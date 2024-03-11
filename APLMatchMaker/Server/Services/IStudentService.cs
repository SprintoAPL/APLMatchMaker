using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Server.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentForListDTO>> GetAsync();
        Task<StudentForListDTO> GetAsync(string id);
        Task<StudentForListDTO> PostAsync(StudentForListDTO dto);
        Task UpdateAsync(string id, StudentForListDTO dto);
    }
}