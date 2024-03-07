using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Repositories;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using APLMatchMaker.Server.Exceptions;
using AutoMapper;

namespace APLMatchMaker.Server.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<StudentForListDTO>> GetAsync()
        {
            var _students = await _studentRepository.GetAsync();
            var dtos = _mapper.Map<IEnumerable<StudentForListDTO>>(_students);
            return dtos;
        }

        public async Task<StudentForListDTO> GetAsync(string id)
        {
            var _student = await _studentRepository.GetAsync(id) ?? throw new StudentNotFoundException(id);
            return _mapper.Map<StudentForListDTO>(_student);
        }

        public async Task<StudentForListDTO> PostAsync(StudentForListDTO dto)
        {
            var _student = _mapper.Map<ApplicationUser>(dto);
            await _studentRepository.AddAsync(_student);
            await _studentRepository.CompleteAsync();
            return _mapper.Map<StudentForListDTO>(_student);
        }

        public async Task UpdateAsync(string id, StudentForListDTO dto)
        {
            var _student = await _studentRepository.GetAsync(id) ?? throw new StudentNotFoundException(id);
            _mapper.Map(dto, _student);
            _studentRepository.Update(_student);
            await _studentRepository.CompleteAsync();
        }

    }
}
