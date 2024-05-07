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
using ProjectManagement.IntegrationalTest.Helpers;

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
                Utilities.InitializeDatabase(dbContext);
            }
        });
    }
}