using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProjectManagement.Application.Services.Projects;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.Data;
using ProjectPulse.DataAccess.Repositories.Projects;
using ProjectPulse.IntegrationalTests.Helpers;

namespace ProjectPulse.IntegrationalTests;

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
            
            services.AddIdentityCore<AppUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectsService, ProjectsService>();

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
                Utilities.InitializeDatabase(dbContext, userManager);
            }
        });
    }
}