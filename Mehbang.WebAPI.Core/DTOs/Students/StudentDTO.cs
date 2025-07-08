namespace Mehbang.WebAPI.Data.DTOs.Students;


public record StudentDTO  
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string NationalCode { get; set; }
    public DateTime BirthDate { get; set; }
}
