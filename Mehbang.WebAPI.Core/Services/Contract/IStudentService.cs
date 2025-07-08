
using Mehbang.WebAPI.Data.DTOs.Students;

namespace Mehbang.WebAPI.Core.Services.Contract;

public interface IStudentService
{
    Task<List<StudentDTO>> GetAllAsync(string? fullname, string? nationalCode);
    Task<StudentDTO?> GetByIdAsync(int id);
    Task<bool> CreateAsync(CreateStudentDTO command);
    Task<int> UpdateAsync(UpdateStudentDTO command);
    Task<bool> DeleteByIdAsync(int id);
}
