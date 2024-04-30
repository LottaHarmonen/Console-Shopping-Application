namespace Labb2ProgTemplate.Models;

public class Logo
{
    public void PrintLogo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\t\tWelcome!\n{"\ud83e\udd8b"} ---Butterfly Beauty Clinic Online Shop--- {"\ud83e\udd8b"}\n");
        Console.ResetColor();
    }
}