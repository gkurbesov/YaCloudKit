namespace YaCloudKit.MQ.Transport.Examples;

public class UserRegistrationDto
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public DateTime RegistrationDateTime { get; set; }
}