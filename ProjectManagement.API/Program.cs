using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Services;
using ProjectManagement.Application.Services.Projects;
using ProjectManagement.Application.Services.Tasks;
using ProjectManagement.Application.Services.Users;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.Repositories.Projects;
using ProjectManagement.DataAccess.Repositories.Tasks;
using ProjectManagement.DataAccess.Repositories.Users;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
    // .AddJsonOptions(options =>
    // {
    //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    // });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnection"), b => b.MigrationsAssembly("ProjectManagement.DataAccess")));

//Repositories
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();

//Services
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IUsersService, UserService>();

//AssignedUser lowercase urls
builder.Services.AddRouting(options => options.LowercaseUrls = true);

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