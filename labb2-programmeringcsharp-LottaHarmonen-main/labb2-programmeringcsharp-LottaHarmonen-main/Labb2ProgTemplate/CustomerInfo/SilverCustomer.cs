using Labb2ProgTemplate.Models;

namespace Labb2ProgTemplate.CustomerInfo;

public class SilverCustomer : Customer
{
    public SilverCustomer(string name, string password) : base(name, password)
    {
    }
    public string SilverCustomerSummary(Customer currentCustomer, Currency currency, String currencyCode)
    {
        double cartTotalConverted = currentCustomer.CartTotal(currentCustomer) / currency.Currencies[currencyCode];
        double cartTotalWithDiscount = cartTotalConverted * 0.90;
        string summary = string.Empty;

        summary += $"\tYour total is: {Math.Round(cartTotalConverted, 2)} {currencyCode}\n";
        summary += $"\t{currentCustomer.Name} you are a silver member and get a 10% discount of your order!\n";
        summary += $"\tYour new total is: {Math.Round(cartTotalWithDiscount, 2)} {currencyCode}";

        return summary;
    }
}