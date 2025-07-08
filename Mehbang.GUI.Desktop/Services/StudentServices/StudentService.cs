using System.Text.Json;
using System.Text;
using Mehbang.GUI.Desktop.Models.Students;

namespace Mehbang.GUI.Desktop.Services.StudentServices;

public class StudentService
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string baseUrl = "https://localhost:7007/api/Students";

    public async Task<List<StudentModel>> GetAllStudentsAsync(string? fullname, string? nationalCode)
    {
        var response = await _httpClient.GetAsync($"{baseUrl}?fullname={fullname}&nationalCode={nationalCode}");
        //response.EnsureSuccessStatusCode();
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"خطا در افزودن دانشجو: {errorContent}");
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer
                .Deserialize<List<StudentModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
    public async Task<StudentModel> GetStudentByIdAsync(int Id)
    {
        var response = await _httpClient.GetAsync(baseUrl + $"/{Id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"خطا در گرفتن لیست دانشجویان: {errorContent}");
        }
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer
            .Deserialize<StudentModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task AddStudentAsync(CreateStudentModel student)
    {
        var content = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(baseUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"خطا در افزودن دانشجو: {errorContent}");
        }
    }

    public async Task UpdateStudentAsync(UpdateStudentModel student)
    {
        var content = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(baseUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"خطا در ویرایش دانشجو: {errorContent}");
        }
    }

    public async Task DeleteStudentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"خطا در حذف دانشجو: {errorContent}");
        }
    }
}
