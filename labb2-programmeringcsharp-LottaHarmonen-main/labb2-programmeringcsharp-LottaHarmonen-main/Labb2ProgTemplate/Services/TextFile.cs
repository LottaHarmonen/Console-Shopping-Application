using System.IO;
using System;
using Labb2ProgTemplate.CustomerInfo;

namespace Labb2ProgTemplate.Services;
public class TextFile
{
    public void AddCustomerToFile(Customer unkownCustomer)
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CustomerInfo.txt");
        using StreamWriter sw = File.AppendText(path);
        {
            sw.WriteLine(unkownCustomer);
        }
    }

    public List<Customer> TakeCustomersFromFile()
    {
        List<Customer> TemporaryCustomerList = new List<Customer>();

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CustomerInfo.txt");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                Customer Kund1 = new GoldCustomer("Knatte", "123");
                sw.WriteLine(Kund1);

                Customer Kund2 = new SilverCustomer("Fnatte", "321");
                sw.WriteLine(Kund2);

                Customer Kund3 = new BronzeCustomer("Tjatte", "213");
                sw.WriteLine(Kund3);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                string? line = "";
                string username = "";
                var password = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');

                    if (line.Contains("Username"))
                    {
                        username = parts[1];
                    }
                    else if (line.Contains("Password"))
                    {
                        password = parts[1];
                    }
                    else if (line.Contains("Membership"))
                    {
                        var membership = parts[1];
                        switch (membership)
                        {
                            case "GoldCustomer":
                                Customer unknown1 = new GoldCustomer(username, password);
                                TemporaryCustomerList.Add(unknown1);
                                break;
                            case "SilverCustomer":
                                Customer unknown2 = new SilverCustomer(username, password);
                                TemporaryCustomerList.Add(unknown2);
                                break;
                            case "BronzeCustomer":
                                Customer unknown3 = new BronzeCustomer(username, password);
                                TemporaryCustomerList.Add(unknown3);
                                break;
                            default:
                                Customer unknown = new Customer(username, password);
                                TemporaryCustomerList.Add(unknown);
                                break;
                        }
                    }
                }
            }
        }
        else if (File.Exists(path))
        {
            using StreamReader sr = new StreamReader(path);
            string? line = "";
            string username = "";
            string password = "";

            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(':');

                if (line.Contains("Username"))
                {
                    username = parts[1];
                }
                else if (line.Contains("Password"))
                {
                    password = parts[1];
                }
                else if (line.Contains("Membership"))
                {
                    var membership = parts[1];
                    switch (membership)
                    {
                        case "GoldCustomer":
                            Customer unknown1 = new GoldCustomer(username, password);
                            TemporaryCustomerList.Add(unknown1);
                            break;
                        case "SilverCustomer":
                            Customer unknown2 = new SilverCustomer(username, password);
                            TemporaryCustomerList.Add(unknown2);
                            break;
                        case "BronzeCustomer":
                            Customer unknown3 = new BronzeCustomer(username, password);
                            TemporaryCustomerList.Add(unknown3);
                            break;
                        default:
                            Customer unknown = new Customer(username, password);
                            TemporaryCustomerList.Add(unknown);
                            break;
                    }
                }
            }
        }
        return TemporaryCustomerList;
    }
}