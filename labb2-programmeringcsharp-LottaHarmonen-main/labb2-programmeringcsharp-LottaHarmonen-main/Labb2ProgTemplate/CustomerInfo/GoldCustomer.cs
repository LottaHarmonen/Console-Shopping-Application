using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Labb2ProgTemplate.Models;

namespace Labb2ProgTemplate.CustomerInfo;

public class GoldCustomer : Customer
{
    public GoldCustomer(string name, string password) : base(name, password)
    {
    }

    public string GoldCustomerSummary(Customer currentCustomer, Currency currency, String currencyCode)
    {
        double cartTotalConverted = currentCustomer.CartTotal(currentCustomer) / currency.Currencies[currencyCode];
        double cartTotalWithDiscount = cartTotalConverted * 0.85;
        string summary = string.Empty;

        summary += $"\tYour total is: {Math.Round(cartTotalConverted, 2)} {currencyCode}\n";
        summary += $"\t{currentCustomer.Name} you are a gold member and get a 15% discount of your order!\n";
        summary += $"\tYour new total is: {Math.Round(cartTotalWithDiscount, 2)} {currencyCode}";

        return summary;
    }
}
