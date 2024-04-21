using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class Project
{
    public Project(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<ProjectTask> Tasks { get; set; } = new();
    public List<ProjectUser> ProjectUsers { get; set; } = new();

    public static Project Create(Guid id, string name, string description)
    {
        //add data verification here
        return new Project(id, name, description);
    }
}