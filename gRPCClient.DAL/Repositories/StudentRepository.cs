using Grpc.Core;
using gRPCClient.Domain.Models;
using gRPCClient.Domain.Repositories;
using gRPCServer.gRPC.Protos;
using static gRPCServer.gRPC.Protos.StudentService;

namespace gRPCClient.DAL.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly StudentServiceClient studentServiceClient;

    public StudentRepository(StudentServiceClient studentServiceClient)
    {
        this.studentServiceClient = studentServiceClient;
    }

    public async IAsyncEnumerable<StudentModel> CreateAsync(IEnumerable<StudentCreate> students)
    {
        var request = studentServiceClient.Create();

        foreach (var student in students)
        {
            await request.RequestStream.WriteAsync(new StudentCreateRequest
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                FatherName = student.FatherName,
                StudentNumber = student.StudentNumber
            });
        }
        await request.RequestStream.CompleteAsync();

        while (await request.ResponseStream.MoveNext())
        {
            var response = request.ResponseStream.Current;
            yield return new StudentModel
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                FatherName = response.FatherName,
                StudentNumber = response.StudentNumber
            };
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var request = await studentServiceClient.DeleteAsync(new GetStudentByIdRequest
        {
            Id = id
        });
        return request.Success;
    }

    public async IAsyncEnumerable<StudentModel> GetAllAsync()
    {
        var request = studentServiceClient.GetAll(new Google.Protobuf.WellKnownTypes.Empty());

        while (await request.ResponseStream.MoveNext())
        {
            var response = request.ResponseStream.Current;
            var student = new StudentModel
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                FatherName = response.FatherName,
                StudentNumber = response.StudentNumber
            };
            yield return student;
        }
    }

    public async Task<StudentModel> GetByIdAsync(int id)
    {
        var request = await studentServiceClient.GetByIdAsync(new GetStudentByIdRequest
        {
            Id = id
        });
        await Task.CompletedTask;
        return new StudentModel
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            FatherName = request.FatherName,
            StudentNumber = request.StudentNumber
        };
    }

    public async Task<bool> UpdateAsync(StudentUpdate student)
    {
        var request = await studentServiceClient.UpdateAsync(new StudentUpdateRequest
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FatherName = student.FatherName
        });
        return request.Success;
    }
}
