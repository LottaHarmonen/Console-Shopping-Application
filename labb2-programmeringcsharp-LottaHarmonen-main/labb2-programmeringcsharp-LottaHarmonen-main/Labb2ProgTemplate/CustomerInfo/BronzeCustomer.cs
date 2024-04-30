using Labb2ProgTemplate.CustomerInfo;
using Labb2ProgTemplate.Models;

namespace Labb2ProgTemplate;

public class BronzeCustomer : Customer
{
    public BronzeCustomer(string name, string password) : base(name, password)
    {
    }
    public string BronzeCustomerSummary(Customer currentCustomer, Currency currency, String currencyCode)
    {
        double cartTotalConverted = currentCustomer.CartTotal(currentCustomer) / currency.Currencies[currencyCode];
        double cartTotalWithDiscount = cartTotalConverted * 0.95;
        string summary = string.Empty;

        summary += $"\tYour total is: {Math.Round(cartTotalConverted, 2)} {currencyCode}\n";
        summary += $"\t{currentCustomer.Name} you are a bronze member and get a 5% discount of your order!\n";
        summary += $"\tYour new total is: {Math.Round(cartTotalWithDiscount, 2)} {currencyCode}";

        return summary;
    }
}