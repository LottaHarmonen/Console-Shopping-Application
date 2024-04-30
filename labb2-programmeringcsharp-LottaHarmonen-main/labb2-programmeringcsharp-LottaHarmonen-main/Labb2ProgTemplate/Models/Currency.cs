namespace Labb2ProgTemplate.Models;

public class Currency
{
    public Dictionary<string, double> Currencies = new();

    public Currency()
    {
        Currencies["USD"] = 10.9;
        Currencies["EUR"] = 11.52;
        Currencies["SEK"] = 1.00;
    }
}