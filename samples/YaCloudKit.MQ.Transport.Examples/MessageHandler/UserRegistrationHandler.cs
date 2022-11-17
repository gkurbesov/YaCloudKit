namespace YaCloudKit.MQ.Transport.Examples;

public class UserRegistrationHandler : IMessageHandler<UserRegistrationDto>
{
    private readonly MusicService _musicService;

    public UserRegistrationHandler(MusicService musicService)
    {
        _musicService = musicService ?? throw new ArgumentNullException(nameof(musicService));
    }
    
    public async Task HandleAsync(UserRegistrationDto message, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("New user!");
        Console.ResetColor();
        Console.WriteLine($"Id: {message.UserId}");
        Console.WriteLine($"Email: {message.Email}");
        Console.WriteLine($"Time: {message.RegistrationDateTime}");

        await _musicService.HappyBirthday();
    }
}