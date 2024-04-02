using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Repositories;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using APLMatchMaker.Server.Exceptions;
using AutoMapper;
using APLMatchMaker.Server.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using APLMatchMaker.Server.Helpers;

namespace APLMatchMaker.Server.Services
{
    public class StudentService : IStudentService
    {
        //##-< Properties >-###############################################################
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        //#################################################################################


        //##-< Constructor >-##############################################################
        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;

        }
        //#################################################################################


        //##-< Get filtered list of students >-############################################
        public async Task<(IEnumerable<StudentForListDTO>, PagingFactoids)> GetAsync(StudentResourceParameters? studentResourceParameters)
        {
            var _students = await _studentRepository.GetAsync(studentResourceParameters);
            var _pagingFactoids = new PagingFactoids(_students.CurrentPage, _students.TotalPages, _students.TotalCount, _students.PageSize);
            var dtos = _mapper.Map<IEnumerable<StudentForListDTO>>(_students);
            return (dtos, _pagingFactoids);
        }
        //#################################################################################


        //##-< Get a single student with id >-#############################################
        public async Task<StudentForDetailsDTO?> GetAsync(string id)
        {
            var _student = await _studentRepository.GetAsync(id);
            if (_student == null)
            {
                return null;
            }
            return _mapper.Map<StudentForDetailsDTO>(_student);
        }
        //#################################################################################


        //##-< Post a new student >-#############################################################
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

            var ok = await _studentRepository.AddAsync(_student, dto.Password);
            ok = ok && await _studentRepository.CompleteAsync();
            if (!ok)
            {
                throw new CouldNotCreateStudentException();
            }
            return _mapper.Map<StudentForDetailsDTO>(_student);
        }
        //#################################################################################


        //##-< Get a single student with id and return as "ForUpdateDTO" >-################
        public async Task<StudentForUpdateDTO?> GetForUpdateAsync(string id)
        {
            var _student = await _studentRepository.GetAsync(id);
            if (_student == null)
            {
                return null;
            }
            return _mapper.Map<StudentForUpdateDTO>(_student);
        }
        //#################################################################################


        //##-< Update a student >-#############################################################
        public async Task<StudentForDetailsDTO?> UpdateStudentAsync(string _id, StudentForUpdateDTO _studentToPatch)
        {
            var _StudentFromRepo = await _studentRepository.GetAsync(_id);

            if (_StudentFromRepo == null)
            {
                return null;
            }

            _mapper.Map(_studentToPatch, _StudentFromRepo);

            var ok = _studentRepository.UpdateStudent(_StudentFromRepo);
            ok = ok && await _studentRepository.CompleteAsync();

            if (ok)
            {
                return _mapper.Map<StudentForDetailsDTO>(_StudentFromRepo);
            }
            return null;
        }
        //#################################################################################


        //##-< Delete a student with id >-#################################################
        public async Task<bool> RemoveAsync(string id)
        {
            var result = await _studentRepository.RemoveAsync(id);
            return result;
        }
        //#################################################################################


        //##-< Check if the email address already exists. >-###############################
        public async Task<bool> EmailExistAsync(string email)
        {
            return await _studentRepository.EmailExistAsync(email);
        }
        //#################################################################################


        //##-< ???????????? >-#############################################################
        // New methods goes here.
        //#################################################################################
    }
}
