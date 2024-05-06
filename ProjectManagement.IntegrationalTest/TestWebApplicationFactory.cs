using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProjectManagement.Application.Services.Projects;
using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.Repositories.Projects;

namespace ProjectManagement.IntegrationalTest;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("test");
            });
            
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectsService, ProjectsService>();

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                PopulateTestData(dbContext);
            }
        });
    }

    private static void PopulateTestData(ApplicationDbContext dbContext)
    {
        var projects = new List<ProjectEntity>
        {
            new ()
            {
                Id = new Guid("d99b037b-1e3a-4de0-812f-90e35b30f07a"),
                Name = "Project 1",
                Description = "Description for Project 1",
            },
            new ()
            {
                Id = new Guid("18c06c2c-7476-48f0-b9e6-4bcbe8d2a129"),
                Name = "Project 2",
                Description = "Description for Project 2",
            },
            new ()
            {
                Id = new Guid("5ac6bace-5059-4e5d-bfc1-643d0e9c05cf"),
                Name = "Project 3",
                Description = "Description for Project 3",
            }
        };

        dbContext.Projects.AddRange(projects);
        dbContext.SaveChanges();
    }
}