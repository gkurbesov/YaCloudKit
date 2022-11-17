namespace YaCloudKit.MQ.Transport.Examples;

public class NotificationHandler : IMessageHandler<NotificationDto>
{
    private readonly MusicService _musicService;

    public NotificationHandler(MusicService musicService)
    {
        _musicService = musicService ?? throw new ArgumentNullException(nameof(musicService));
    }
    
    public async Task HandleAsync(NotificationDto message, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Receive notification");
        Console.ResetColor();
        Console.WriteLine($"Id: {message.Id}");
        Console.WriteLine($"Tite: {message.Title}");
        Console.WriteLine($"Message: {message.Message}");

        await _musicService.StarWars();
    }
}