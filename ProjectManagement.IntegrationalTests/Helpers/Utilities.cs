using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.Data;

namespace ProjectManagement.IntegrationalTests.Helpers;

public class Utilities
{
    public static void InitializeDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        PopulateTestData(context);
    }
    
    public static void Cleanup(ApplicationDbContext dbContext)
    {
        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.Users.RemoveRange(dbContext.Users);
        dbContext.ProjectTasks.RemoveRange(dbContext.ProjectTasks);
        dbContext.SaveChanges();
        PopulateTestData(dbContext);
    }
    
    public static void PopulateTestData(ApplicationDbContext dbContext)
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

        var users = new List<UserEntity>()
        {
            new ()
            {
                Id = new Guid("fcd21c1e-914c-4a6f-aa18-41505d29c8e7"),
                FirstName = "Timofei",
                LastName = "Korsakov",
                Email = "tkorsakov77@gmail.com",
                Login = "skmkqw",
                Password = "040412006"
            },
            new ()
            {
                Id = new Guid("7ec8d9b7-17a3-4855-8d17-7fb0a6c037d4"),
                FirstName = "Denis",
                LastName = "Rezanko",
                Email = "rezden22@gmail.com",
                Login = "rezden",
                Password = "17092005"
            },
            new ()
            {
                Id = new Guid("b1b2a921-af2a-4e38-a6e9-3d38e540dca9"),
                FirstName = "Artem",
                LastName = "Novikov",
                Email = "artnov11@gmail.com",
                Login = "artnov",
                Password = "26082005"
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
                ProjectId = new Guid("18c06c2c-7476-48f0-b9e6-4bcbe8d2a129")
            },
            
            new ()
            {
                Id = new Guid("2e9f7e98-4870-4c04-9e9b-5946f2a30ae9"),
                Title = "Task 3",
                Description = "Description for task 3",
                ProjectId = new Guid("5ac6bace-5059-4e5d-bfc1-643d0e9c05cf")
            }
        };

        dbContext.Projects.AddRange(projects);
        dbContext.Users.AddRange(users);
        dbContext.ProjectTasks.AddRange(tasks);
        dbContext.SaveChanges();
    }
}