namespace UserAccessManagement.Application.DTOs
{
    public class GrantAccessDto
    {
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
        public string AccessLevel { get; set; } = "Read"; // Read, Write, Admin
    }
}
