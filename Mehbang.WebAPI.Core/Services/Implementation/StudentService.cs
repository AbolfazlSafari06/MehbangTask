using Mehbang.WebAPI.Core.Services.Contract;
using Mehbang.WebAPI.Data.DTOs.Students;
using Mehbang.WebAPI.Data.Entities.Students;
using Mehbang.WebAPI.Data.Repositories.Contract;

namespace Mehbang.WebAPI.Application.Services.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _userRepository;

    public StudentService(IStudentRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CreateAsync(CreateStudentDTO command)
    {
        // Check NationalCode Do Not Exist
        var isNationalCodeExist = await _userRepository
            .IsExistAsync(x => x.NationalCode.Equals(command.NationalCode));

        if (isNationalCodeExist)
        {
            throw new Exception("NationalCode Is Already Exist");
        }

        var newStudent = new Student(command.FullName, command.NationalCode, command.BirthDate);
        var result = await _userRepository.CreateAsync(newStudent);
        await _userRepository.SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var student = await _userRepository.GetByIdAsync(id);
        if (student is null)
        {
            throw new Exception("Student Does Not Exist");
        }
        _userRepository.Delete(student);
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<List<StudentDTO>> GetAllAsync(string? fullname, string? nationalCode)
    {
        var students = await _userRepository.GetAllAsync(fullname,nationalCode);
        return students.Select(x => new StudentDTO
        {
            Id = x.Id,
            FullName = x.FullName,
            BirthDate = x.BirthDate,
            NationalCode = x.NationalCode,
        }).ToList();
    }

    public async Task<StudentDTO?> GetByIdAsync(int id)
    {
        var student = await _userRepository.GetByIdAsync(id);
        if (student is null)
        {
            throw new Exception("Student Does Not Exist");
        }

        return new StudentDTO
        {
            Id = student.Id,
            FullName = student.FullName,
            BirthDate = student.BirthDate,
            NationalCode = student.NationalCode,
        };
    }

    public async Task<int> UpdateAsync(UpdateStudentDTO command)
    {
        var student = await _userRepository.GetByIdAsync(command.Id);
        if (student is null)
        {
            throw new Exception("Student Does Not Exist");
        }

        // Check NationalCode Is Not Duplicated.
        var isNationalCodeDuplicated = await _userRepository
            .IsExistAsync(x => 
            x.NationalCode.Equals(command.NationalCode) 
            && 
            x.Id.Equals(command.Id) == false
            );

        if (isNationalCodeDuplicated)
        {
            throw new Exception("NationalCode Is Already Exist");
        }


        student.SetFullName(command.FullName);
        student.SetNationalCode(command.NationalCode);
        student.SetBirthDate(command.BirthDate);
        await _userRepository.SaveChangesAsync();
        return student.Id;
    }
}
