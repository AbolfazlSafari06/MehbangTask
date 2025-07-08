using Mehbang.WebAPI.Data.Entities.Students;
using Mehbang.WebAPI.Data.Repositories.Contract;
using Mehbang.WebAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mehbang.WebAPI.Data.Repositories.Implementation;

public class StudentRepository : IStudentRepository
{
    private readonly DataBaseContext _context;
    public StudentRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<bool> IsExistAsync(Expression<Func<Student, bool>> query)
    {
        return await _context
            .Students
            .AnyAsync(query);
    }
     
    public async Task<List<Student>> GetAllAsync(string? fullname, string? nationalCode)
    {
        var query = _context.Students.AsNoTracking();
        if (string.IsNullOrEmpty(fullname) == false)
        {
            query = query.Where(x => x.FullName.Contains(fullname.Trim()));
        }
        if (string.IsNullOrEmpty(nationalCode) == false)
        {
            query = query.Where(x => x.NationalCode.Contains(nationalCode.Trim()));
        } 
        return await query.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context
        .Students
        .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CreateAsync(Student command)
    {
        await _context.Students.AddAsync(command);
        return true;
    }
     
    public void Delete(Student command)
    {
        _context.Students.Remove(command);
    }
}
