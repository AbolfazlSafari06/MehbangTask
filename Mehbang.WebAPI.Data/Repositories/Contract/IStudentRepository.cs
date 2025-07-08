using Mehbang.WebAPI.Data.Entities.Students;
using System.Linq.Expressions;

namespace Mehbang.WebAPI.Data.Repositories.Contract;

public interface IStudentRepository 
{
    Task<Student?> GetByIdAsync(int id);
    Task<List<Student>> GetAllAsync(string? fullname, string? nationalCode);
    Task<bool> CreateAsync(Student command);
    Task<bool> IsExistAsync(Expression<Func<Student, bool>> query);
    Task SaveChangesAsync();
    void Delete(Student command);
}
