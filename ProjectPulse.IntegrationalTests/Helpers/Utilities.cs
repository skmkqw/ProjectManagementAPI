using Microsoft.AspNetCore.Identity;
using ProjectPulse.Core.Entities;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.Data;
using Exception = System.Exception;

namespace ProjectPulse.IntegrationalTests.Helpers;

public class Utilities
{
    public static void InitializeDatabase(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        try
        {
            PopulateTestData(context, userManager).Wait();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }
    
    public static void Cleanup(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
    {
        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.ProjectTasks.RemoveRange(dbContext.ProjectTasks);
        dbContext.ProjectUsers.RemoveRange(dbContext.ProjectUsers);
        
        var users = userManager.Users.ToList();
        foreach (var user in users)
        {
            var result = userManager.DeleteAsync(user).Result;
            if (!result.Succeeded)
            {
                Console.WriteLine($"Error deleting user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        
        dbContext.SaveChanges();
        PopulateTestData(dbContext, userManager).Wait();
    }
    
    public static async Task PopulateTestData(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
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

        var users = new List<AppUser>()
        {
            new ()
            {
                Id = new Guid("fcd21c1e-914c-4a6f-aa18-41505d29c8e7"),
                Email = "example1@gmail.com",
                UserName = "user1",
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new ()
            {
                Id = new Guid("7ec8d9b7-17a3-4855-8d17-7fb0a6c037d4"),
                Email = "example2@gmail.com",
                UserName = "user2",
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new ()
            {
                Id = new Guid("b1b2a921-af2a-4e38-a6e9-3d38e540dca9"),
                Email = "example3@gmail.com",
                UserName = "user3",
                SecurityStamp = Guid.NewGuid().ToString()
            }
        };

        var tasks = new List<ProjectTaskEntity>()
        {
            new ()
            {
                Id = new Guid("9f4a9b16-45b1-4895-a3c6-8e2eb8deae58"),
                Title = "Task 1",
                Description = "Description for task 1",
                ProjectId = new Guid("d99b037b-1e3a-4de0-812f-90e35b30f07a")
            },
            
            new ()
            {
                Id = new Guid("6c12bbf9-3a8a-4f7c-9e19-9e0b5b27a08a"),
                Title = "Task 2",
                Description = "Description for task 2",
                ProjectId = new Guid("18c06c2c-7476-48f0-b9e6-4bcbe8d2a129"),
                AssignedUserId = new Guid("fcd21c1e-914c-4a6f-aa18-41505d29c8e7")
            },
            
            new ()
            {
                Id = new Guid("2e9f7e98-4870-4c04-9e9b-5946f2a30ae9"),
                Title = "Task 3",
                Description = "Description for task 3",
                ProjectId = new Guid("5ac6bace-5059-4e5d-bfc1-643d0e9c05cf"),
                Status = TaskStatuses.Done,
                AssignedUserId = new Guid("18c06c2c-7476-48f0-b9e6-4bcbe8d2a129")
            }
        };

        var projectUsers = new List<ProjectUserEntity>()
        {
            new ()
            {
                ProjectId = new Guid("d99b037b-1e3a-4de0-812f-90e35b30f07a"),
                UserId = new Guid("fcd21c1e-914c-4a6f-aa18-41505d29c8e7")
            },
            
            new ()
            {
                ProjectId = new Guid("18c06c2c-7476-48f0-b9e6-4bcbe8d2a129"),
                UserId = new Guid("7ec8d9b7-17a3-4855-8d17-7fb0a6c037d4")
            }
        };

        
        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Password123!");
        }
        
        dbContext.Projects.AddRange(projects);
        dbContext.ProjectTasks.AddRange(tasks);
        dbContext.ProjectUsers.AddRange(projectUsers);
        await dbContext.SaveChangesAsync();
    }
}