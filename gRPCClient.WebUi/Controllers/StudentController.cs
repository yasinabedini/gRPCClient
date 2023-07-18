using gRPCClient.Domain.Models;
using gRPCClient.Domain.Services;
using gRPCServer.gRPC.Protos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gRPCClient.WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public IActionResult Create(List<StudentModel> students)
        {
            List<StudentCreate> newStudents = new List<StudentCreate>();
            foreach (var student in students)
            {
                newStudents.Add(new StudentCreate
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    FatherName = student.FatherName,
                    StudentNumber = student.StudentNumber
                });
            }
            studentService.CreateAsync(newStudents);
            return Ok(newStudents);
        }
    }
}
