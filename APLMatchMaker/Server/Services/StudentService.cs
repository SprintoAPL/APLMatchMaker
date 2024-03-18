using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Repositories;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using APLMatchMaker.Server.Exceptions;
using AutoMapper;
using APLMatchMaker.Server.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APLMatchMaker.Server.Services
{
    public class StudentService : IStudentService
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

        public async Task<IEnumerable<StudentForListDTO>> GetAsync(StudentResourceParameters? studentResourceParameters)
        {
            var _students = await _studentRepository.GetAsync(studentResourceParameters);
            var dtos = _mapper.Map<IEnumerable<StudentForListDTO>>(_students);
            return dtos;
        }

        public async Task<StudentForDetailsDTO?> GetAsync(string id)
        {
            var _student = await _studentRepository.GetAsync(id);
            if (_student == null)
            {
                return null;
            }
            return _mapper.Map<StudentForDetailsDTO>(_student);
        }

        public async Task<StudentForDetailsDTO> PostAsync(StudentForCreateDTO dto)
        {
            var _student = new ApplicationUser
            {
                IsStudent = true,
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = dto.Email.ToLower().Trim(),
                UserName = dto.Email.Trim(),
                EmailConfirmed = true,
                PhoneNumber = dto.PhoneNumber!.Trim(),
                StudentSocSecNo = dto.StudentSocSecNo!.Trim(),
                Address = dto.Address!.Trim(),
                Language = dto.Language!.Trim(),
                Nationality = dto.Nationality!.Trim()
            };

            //var _student = _mapper.Map<ApplicationUser>(dto);
            var ok = await _studentRepository.AddAsync(_student, dto.Password);
            ok = ok && await _studentRepository.CompleteAsync();
            if (!ok)
            {
                throw new CouldNotCreateStudentException();
            }
            return _mapper.Map<StudentForDetailsDTO>(_student);
        }

        public async Task<StudentForDetailsDTO?> PartiallyUpdateStudentAsync(string id, JsonPatchDocument<StudentForUpdateDTO> _patchDocument)
        {
            var _studentFromRepo = await _studentRepository.GetStudentToUpdateAsync(id);
            if (_studentFromRepo == null)
            {
                return null;
            }
            StudentForUpdateDTO _studentToPatch = _mapper.Map<StudentForUpdateDTO>(_studentFromRepo);
            _patchDocument.ApplyTo(_studentToPatch);
            ApplicationUser _StudentToUpdate = _mapper.Map(_studentToPatch, _studentFromRepo);
            var _ok = await _studentRepository.UpdateStudentAsync(_StudentToUpdate);
            _ok = _ok && await _studentRepository.CompleteAsync();

            if (_ok)
            {
                return _mapper.Map<StudentForDetailsDTO>(_StudentToUpdate);
            }
            return null;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var result = await _studentRepository.RemoveAsync(id);
            return result;
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await _studentRepository.EmailExistAsync(email);
        }
    }
}
