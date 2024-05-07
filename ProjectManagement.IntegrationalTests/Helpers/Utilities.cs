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

        dbContext.Projects.AddRange(projects);
        dbContext.SaveChanges();
    }
}