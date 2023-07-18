using gRPCClient.Domain.Models;
using gRPCClient.Domain.Repositories;
using gRPCClient.Domain.Services;

namespace gRPCClient.BLL.Servies;
public class StudentService: IStudentService
{
    private readonly IStudentRepository studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public async IAsyncEnumerable<StudentModel> CreateAsync(IEnumerable<StudentCreate> students)
    {
        var result = studentRepository.CreateAsync(students);
        await foreach (var student in result)
        {
            var newStudent = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber,
            };
            yield return newStudent;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {           
        return await studentRepository.DeleteAsync(id);
    }

    public async IAsyncEnumerable<StudentModel> GetAllAsync()
    {
        await foreach (var student in studentRepository.GetAllAsync())
        {
            var newStudent = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber,
            };
            yield return newStudent;
        }
    }

    public async Task<StudentModel> GetByIdAsync(int id)
    {
        return await studentRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(StudentUpdate student)
    {
        return await studentRepository.UpdateAsync(student);
    }
}
