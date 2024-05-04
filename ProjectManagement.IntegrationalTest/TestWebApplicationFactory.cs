using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.Data;

namespace ProjectManagement.IntegrationalTest;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Data Source=localhost;Initial Catalog=ProjectManagementDTest;User Id=sa;Password=skmkqw04012006Tima;Integrated Security=True;TrustServerCertificate=true;Trusted_Connection=false"); // Замените "your_connection_string" на вашу строку подключения к SQL Server
            });

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

                try
                {
                    dbContext.Database.EnsureCreated();
                    PopulateTestData(dbContext);
                }
                catch (Exception)
                {
                    Console.WriteLine("An error occurred creating the database.");
                }
            }
        });
    }

    private static void PopulateTestData(ApplicationDbContext dbContext)
    {
        var projects = new List<ProjectEntity>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Project 1",
                Description = "Description for Project 1",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Project 2",
                Description = "Description for Project 2",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Project 3",
                Description = "Description for Project 3",
            }
        };

        dbContext.Projects.AddRange(projects);
        dbContext.SaveChanges();
    }
}