namespace Mehbang.WebAPI.Data.DTOs.Students;

public record CreateStudentDTO
{
    public string FullName { get; set; }
    public string NationalCode { get; set; }
    public DateTime BirthDate { get; set; }
}
