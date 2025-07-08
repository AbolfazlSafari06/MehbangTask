namespace Mehbang.WebAPI.Data.Entities.Students;

public class Student 
{
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string NationalCode { get; private set; }
    public DateTime BirthDate { get; private set; }

    public Student(string fullName, string nationalCode, DateTime birthDate)
    {
        SetFullName(fullName);
        SetNationalCode(nationalCode);
        SetBirthDate(birthDate); 
    }

    public void SetFullName(string fullName) {
        if (string.IsNullOrEmpty(fullName) == false) { 
            FullName = fullName;
        }
    }

    public void SetNationalCode(string nationalCode)
    {
        if (string.IsNullOrEmpty(nationalCode) == false)
        {
            NationalCode = nationalCode;
        }
    }

    public void SetBirthDate(DateTime birthDate)
    {
        BirthDate = birthDate;
    }
}
