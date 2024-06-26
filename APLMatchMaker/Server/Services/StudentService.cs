﻿using APLMatchMaker.Server.Exceptions;
using APLMatchMaker.Server.Helpers;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Repositories;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using AutoMapper;

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
        public async Task<StudentForDetailsDTO?> GetAsync(Guid id)
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
                PhoneNumber = dto.PhoneNumber?.Trim(),
                StudentSocSecNo = dto.StudentSocSecNo?.Trim(),
                Address = dto.City?.Trim(),
                Language = dto.Language?.Trim(),
                Nationality = dto.Nationality?.Trim()
            };

            var ok = await _studentRepository.AddAsync(_student);
            ok = ok && await _studentRepository.CompleteAsync();
            if (!ok)
            {
                throw new CouldNotCreateStudentException();
            }
            return _mapper.Map<StudentForDetailsDTO>(_student);
        }
        //#################################################################################


        //##-< Get a single student with id and return as "ForUpdateDTO" >-################
        public async Task<StudentForUpdateDTO?> GetForUpdateAsync(Guid id)
        {
            var _student = await _studentRepository.GetAsync(id);
            if (_student == null)
            {
                return null;
            }
            return _mapper.Map<StudentForUpdateDTO>(_student);
        }
        //#################################################################################


        //##-< Update a student >-#########################################################
        public async Task<StudentForDetailsDTO?> UpdateStudentAsync(Guid _id, StudentForUpdateDTO _updatedStudent)
        {
            if ( _id == Guid.Empty || _updatedStudent == null)
            {
                return null;
            }
            var _studentFromRepo = await _studentRepository.GetAsync(_id);
            if (_studentFromRepo == null)
            {
                return null;
            }
            _mapper.Map(_updatedStudent, _studentFromRepo);

            var ok = _studentRepository.UpdateStudent(_studentFromRepo);
            ok = ok && await _studentRepository.CompleteAsync();

            if (ok)
            {
                return _mapper.Map<StudentForDetailsDTO>(_studentFromRepo);
            }
            return null;
        }
        //#################################################################################


        //##-< Patch a student >-##########################################################
        public async Task<StudentForDetailsDTO?> PatchStudentAsync(Guid _id, StudentForUpdateDTO _studentPatch)
        {
            var _studentFromRepo = await _studentRepository.GetAsync(_id);

            if (_studentFromRepo == null)
            {
                return null;
            }

            _mapper.Map(_studentPatch, _studentFromRepo);

            var ok = _studentRepository.UpdateStudent(_studentFromRepo);
            ok = ok && await _studentRepository.CompleteAsync();

            if (ok)
            {
                return _mapper.Map<StudentForDetailsDTO>(_studentFromRepo);
            }
            return null;
        }
        //#################################################################################


        //##-< Delete a student with id >-#################################################
        public async Task<bool> RemoveAsync(Guid id)
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


        //##-< Check if the student has engagements >-#####################################
        public async Task<bool> HasEngagementsAsync(Guid id)
        {
            return await _studentRepository.HasEngagementsAsync(id);
        }
        //#################################################################################


        //##-< ???????????? >-#############################################################
        // New methods goes here.
        //#################################################################################
    }
}
