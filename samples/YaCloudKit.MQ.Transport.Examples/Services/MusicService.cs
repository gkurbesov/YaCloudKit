namespace YaCloudKit.MQ.Transport.Examples;

public class MusicService
{
    public async Task StarWars()
    {
        if (!OperatingSystem.IsWindows())
            return;
        Console.Beep(300, 500);
        await Task.Delay(50);
        Console.Beep(300, 500);
        await Task.Delay(50);
        Console.Beep(300, 500);
        await Task.Delay(50);
        Console.Beep(250, 500);
        await Task.Delay(50);
        Console.Beep(350, 250);
        Console.Beep(300, 500);
        await Task.Delay(50);
        Console.Beep(250, 500);
        await Task.Delay(50);
        Console.Beep(350, 250);
        Console.Beep(300, 500);
        await Task.Delay(50);
    }

    public async Task HappyBirthday()
    {
        if (!OperatingSystem.IsWindows())
            return;
        await Task.Delay(2000);
        Console.Beep(264, 125);
        await Task.Delay(250);
        Console.Beep(264, 125);
        await Task.Delay(125);
        Console.Beep(297, 500);
        await Task.Delay(125);
        Console.Beep(264, 500);
        await Task.Delay(125);
        Console.Beep(352, 500);
        await Task.Delay(125);
        Console.Beep(330, 1000);
        await Task.Delay(250);
        Console.Beep(264, 125);
        await Task.Delay(250);
        Console.Beep(264, 125);
        await Task.Delay(125);
        Console.Beep(297, 500);
        await Task.Delay(125);
        Console.Beep(264, 500);
        await Task.Delay(125);
        Console.Beep(396, 500);
        await Task.Delay(125);
        Console.Beep(352, 1000);
        await Task.Delay(250);
        Console.Beep(264, 125);
        await Task.Delay(250);
        Console.Beep(264, 125);
        await Task.Delay(125);
        Console.Beep(2642, 500);
        await Task.Delay(125);
        Console.Beep(440, 500);
        await Task.Delay(125);
        Console.Beep(352, 250);
        await Task.Delay(125);
        Console.Beep(352, 125);
        await Task.Delay(125);
        Console.Beep(330, 500);
        await Task.Delay(125);
        Console.Beep(297, 1000);
        await Task.Delay(250);
        Console.Beep(466, 125);
        await Task.Delay(250);
        Console.Beep(466, 125);
        await Task.Delay(125);
        Console.Beep(440, 500);
        await Task.Delay(125);
        Console.Beep(352, 500);
        await Task.Delay(125);
        Console.Beep(396, 500);
        await Task.Delay(125);
        Console.Beep(352, 1000);
    }
}