using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class User
{
    public User(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public List<ProjectUser> ProjectUsers { get; set; } = new();

    public static User Create(Guid id, string firstName, string lastName)
    {
        return new User(id, firstName, lastName);
    }
}