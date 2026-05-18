namespace SchoolManagement.Domain.Entities;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string SchoolName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string? LogoUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Student> Students { get; set; } = new List<Student>();

    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    public ICollection<Class> Classes { get; set; } = new List<Class>();

    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}