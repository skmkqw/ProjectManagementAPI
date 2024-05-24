using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Application.Services.Accounts;
using ProjectManagement.Application.Services.Projects;
using ProjectManagement.Application.Services.Tasks;
using ProjectManagement.Application.Services.Users;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.Data;
using ProjectPulse.DataAccess.Repositories.Accounts;
using ProjectPulse.DataAccess.Repositories.Projects;
using ProjectPulse.DataAccess.Repositories.Tasks;
using ProjectPulse.DataAccess.Repositories.Users;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ProjectPulse.DataAccess")));

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{ 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{ 
    var secret = builder.Configuration["JwtConfig:Secret"]; 
    var issuer = builder.Configuration["JwtConfig:ValidIssuer"]; 
    var audience = builder.Configuration["JwtConfig:ValidAudiences"]; 
    if (secret is null || issuer is null || audience is null) 
    { 
        throw new ApplicationException("Jwt is not set in the configuration"); 
    } 
    options.SaveToken = true; 
    options.RequireHttpsMetadata = false; 
    options.TokenValidationParameters = new TokenValidationParameters() 
    { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidAudience = audience, 
        ValidIssuer = issuer, 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)) 
    };
});

//Repositories
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();

//Services
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();

//AssignedUser lowercase urls
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program() {}