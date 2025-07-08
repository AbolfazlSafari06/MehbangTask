namespace Mehbang.WebAPI.Data.DTOs.Students;

public record UpdateStudentDTO : CreateStudentDTO
{
    public int Id { get; set; }
}
