using gRPCClient.Domain.Models;
using gRPCClient.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace gRPCClient.WebApi.Controllers;

[Route("api[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    [HttpPost]
    public IActionResult Create(List<StudentCreate> students)
    {
        return Ok(studentService.CreateAsync(students));
    }

    [HttpGet]
    public IAsyncEnumerable<StudentModel> GetAll()
    {
        return studentService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<StudentModel> GetById(int id)
    {
        var result = await studentService.GetByIdAsync(id);
        return result;
    }

    [HttpDelete]
    public async Task<bool> Delete(int id)
    {
        return await studentService.DeleteAsync(id);
    }

    [HttpPatch]
    public async Task<bool> Update(StudentUpdate student)
    {
        return await studentService.UpdateAsync(student);
    }
}