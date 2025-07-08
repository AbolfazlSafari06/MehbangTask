using Mehbang.WebAPI.Core.Services.Contract;
using Mehbang.WebAPI.Data.DTOs.Students;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Mehbang.WebAPI.Web.Controllers;

[Route("api/Students")]
[ApiController]
public class StudentsController : ControllerBase
{ 
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/<StudentsController>
    [HttpGet]
    public async Task<ActionResult<List<StudentDTO>>> GetAllAsync(string? fullname, string? nationalCode)
    {
        var students = await _studentService.GetAllAsync(fullname, nationalCode);
        return students;
    }

    // GET api/<StudentsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> Get([Required]int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null)
            return NotFound();

        return student;
    }

    // POST api/<StudentsController>
    [HttpPost]
    public async Task<ActionResult> Post([Required,FromBody] CreateStudentDTO command)
    { 
        var student = await _studentService.CreateAsync(command);

        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    // PUT api/<StudentsController>/5
    [HttpPut]
    public async Task<ActionResult> Update([Required,FromBody] UpdateStudentDTO command)
    { 
        var student = await _studentService.UpdateAsync(command);

        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    // DELETE api/<StudentsController>/5
    [HttpDelete("{id}")]
    public async Task Delete([Required] int id)
    { 
        await _studentService.DeleteByIdAsync(id);
    }
}
