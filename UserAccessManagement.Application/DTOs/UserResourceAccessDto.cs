namespace UserAccessManagement.Application.DTOs
{
    public class UserResourceAccessDto
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
    }
}
