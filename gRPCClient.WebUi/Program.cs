using gRPCClient.DAL.Repositories;
using gRPCClient.Domain.Repositories;
using gRPCClient.Domain.Services;
using gRPCClient.WebUi.Controllers;
using static gRPCServer.gRPC.Protos.StudentService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, gRPCClient.BLL.Servies.StudentServicecs>();
builder.Services.AddGrpcClient<StudentServiceClient>(t =>
{
    t.Address = new Uri("https://localhost:7130");
});


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
