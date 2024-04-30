using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Labb2ProgTemplate.CustomerInfo;
using Labb2ProgTemplate.Models;

namespace Labb2ProgTemplate.Services
{
    public class Shop
    {
        public List<Customer> ExistingCustomers { get; set; }
        private Customer CurrentCustomer { get; set; }
        private List<Product> Products { get; set; }


        TextFile textFile = new TextFile();
        Currency currencies = new Currency();
        Logo printLogo = new Logo();

        Product nailPolish = new Product("Caught in the Nude Nail Polish 15ml", 109.99);
        Product faceMask = new Product("Tata Harper’s Hydrating Floral Mask", 849.99);
        Product cream = new Product("Kiehl's Ultra Facial Cream",549.00);
        Product superstarGlowKit = new Product("Superstar Glow Kit 2-Pack", 299.99);
        Product lipBalm = new Product("La Roche-Posay Cicaplast Baume Lips 7.5ml", 85.00);

        public Shop()
        {
            ExistingCustomers = textFile.TakeCustomersFromFile();
            Products = new List<Product>
            {
                nailPolish,
                faceMask,
                cream,
                superstarGlowKit,
                lipBalm
            };
        }

        public void MainMenu()
        {
            printLogo.PrintLogo();
            Console.WriteLine("\t\t1. Log in\n\t\t2. Register");

            while (true)
            {
                var userChoice = Console.ReadLine();

                if (userChoice == "1")
                {
                    Login();
                }
                else if (userChoice == "2")
                {
                    Register();
                }
                else
                {
                    Console.WriteLine("\nInvalid choice, choose between 1. Log In and 2. Register");
                }
            }
        }

        private void Login()
        {
            printLogo.PrintLogo();
            bool usernameMatchFound = false;
            bool passwordCorrect = false;

            Console.WriteLine("\t\tLog in\n\tType in your username:");
            string inputUsername = Console.ReadLine();

            foreach (Customer customer in ExistingCustomers)
            {
                if (inputUsername == customer.Name)
                {
                    usernameMatchFound = true;
                    Console.Clear();
                    printLogo.PrintLogo();
                    while (!passwordCorrect)
                    {
                        Console.WriteLine("\t\tLog in\n\tType in your password: ");
                        var inputPassword = Console.ReadLine();
                        passwordCorrect = customer.CheckPassword(inputPassword, customer);

                        if (passwordCorrect)
                        {
                            CurrentCustomer = customer;
                            ShopMenu();
                        }
                        else passwordCorrect = false;
                        {
                            Console.WriteLine("Incorrect password, try again");
                        }
                    }
                }
            }

            if (!usernameMatchFound)
            {
                printLogo.PrintLogo();
                Console.WriteLine("Oops! Username does not exist. Would you like to register an account?\n1. Yes\n2. No\n3. Try again");
                while (true)
                {
                    var inputAnswer = Console.ReadLine();

                    if (inputAnswer == "1")
                    {
                        Register();
                    }
                    else if (inputAnswer == "2")
                    {
                        Console.Clear();
                        MainMenu();
                    }
                    else if (inputAnswer == "3")
                    {
                        Login();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice, choose between 1. Yes, 2. No and 3. Try again");
                    }
                }
            }
        }

        private void Register()
        {
            printLogo.PrintLogo();
            Console.WriteLine("\tCreate an account to start shopping\n\tUsername: ");
            
            while (true)
            {
                string username = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be empty or contain only spaces. Please try again.");
                }

                else
                {
                    printLogo.PrintLogo();
                    Console.WriteLine("\tCreate an account to start shopping\n\tPassword: ");
                    var password = Console.ReadLine();
                    Customer newCustomer = new Customer(username, password);

                    textFile.AddCustomerToFile(newCustomer);
                    ExistingCustomers.Add(newCustomer);

                    Login();
                }
            }
        }

        private void ShopMenu()
        {
            printLogo.PrintLogo();
            Console.WriteLine($"\t\tHello, {CurrentCustomer.Name}!\n\t1. Shop\n\t2. View your cart\n\t3. Check out\n\t4. Log out - Your shopping cart will be saved");
            string currencyKey = "SEK";

            while (true)
            {
                var inputChoice = Console.ReadLine();

                if (inputChoice == "1")
                {
                    printLogo.PrintLogo();
                    int choiceNumber = 1;
                    Console.WriteLine("\tInsert item number and press enter to add items to your cart");
                    for (int i = 0; i < Products.Count; i++)
                    {
                        Console.WriteLine($"\t{choiceNumber}. { Products[i].Name} - { Products[i].Price} {currencyKey}");
                        choiceNumber++;
                    }

                    var returnToShopMenu = choiceNumber;
                    Console.WriteLine($"\n\t{returnToShopMenu}. Return to shop menu\n");

                    while (true)
                    {
                        int choiceAsIndex = Convert.ToInt32(Console.ReadLine());
                        if (choiceAsIndex == Products.Count + 1)
                        {
                            ShopMenu();
                        }
                        else if (choiceAsIndex > Products.Count + 1 || choiceAsIndex < 1)
                        {
                            Console.WriteLine("Invalid choice, choose between presented items");
                        }
                        else
                        {
                            CurrentCustomer.AddToCart(Products[choiceAsIndex - 1]);
                        }
                    }
                }
                else if (inputChoice == "2")
                {
                    ViewCart();
                }
                else if (inputChoice == "3")
                {
                    Console.Clear();
                    Checkout();
                }
                else if (inputChoice == "4")
                {
                    Console.Clear();
                    CurrentCustomer = null!;
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Invalid choice, choose between 1. To Shop, 2. To view your cart 3. To continue to check out 4. To log out");
                }
            }
        }

        private void ViewCart()
        {
            printLogo.PrintLogo();
            Console.WriteLine("Would you like to view your cart items in another currency?\n\t1. To continue with swedish crowns(SEK)\n\t2. Euros(EUR)\n\t3. Dollars(USD)\n");
            int inputChoice = Convert.ToInt32(Console.ReadLine());
            string choiceAsKey = string.Empty;

            switch (inputChoice)
            {
                case 1:
                {
                    choiceAsKey = "SEK";
                    break;
                }
                case 2:
                {
                    choiceAsKey = "EUR";
                    break;
                }
                case 3:
                {
                    choiceAsKey = "USD";
                    break;
                }
            }

            printLogo.PrintLogo();
            Console.WriteLine("\tYour cart items:");
            int counter = 0;

            foreach (Product product in Products)
            {
                foreach (Product productCart in CurrentCustomer.Cart)
                {
                    if (product == productCart)
                    {
                        counter++;
                    }
                }
                if (counter > 0)
                {
                    Console.WriteLine($"\t{product.Name} {counter}qty {Math.Round(product.Price / currencies.Currencies[choiceAsKey], 2)} {choiceAsKey}/per item = {counter * Math.Round(product.Price / currencies.Currencies[choiceAsKey], 2)} {choiceAsKey}");
                }
                counter = 0;
            }
            Console.WriteLine($"\tTotal: {Math.Round(CurrentCustomer.CartTotal(CurrentCustomer) / currencies.Currencies[choiceAsKey], 2)} {choiceAsKey}\n\n1. Proceed to check out 2. Continue shopping 3. Remove items from your cart");

            while (true)
            {
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Checkout();
                }
                else if (choice == "2")
                {
                    ShopMenu();
                }
                else if (choice == "3")
                {
                    while (true)
                    {
                        printLogo.PrintLogo();
                        int choicenumber = 1;
                        Console.WriteLine("\tInsert item number and press enter to remove the item from your cart");

                        for (int i = 0; i < CurrentCustomer.Cart.Count; i++)
                        {
                            Console.WriteLine($"\t{choicenumber}. {CurrentCustomer.Cart[i].Name} - {Math.Round(CurrentCustomer.Cart[i].Price / currencies.Currencies[choiceAsKey], 2)} {choiceAsKey}");
                            choicenumber++;
                        }
                        Console.WriteLine($"\n\t{choicenumber}. Return to shop menu");

                        int choiceAsIndex = Convert.ToInt32(Console.ReadLine());
                        if (choiceAsIndex > CurrentCustomer.Cart.Count + 1)
                        {
                            Console.WriteLine("Invalid choice, choose between presented items");
                            Thread.Sleep(1000);
                        }
                        else if (choiceAsIndex == choicenumber)
                        {
                            ShopMenu();
                        }
                        else
                        {
                            CurrentCustomer.RemoveFromCart(CurrentCustomer.Cart[choiceAsIndex - 1]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice, choose between 1. To proceed to check out 2. To Continue shopping and 3. To remove items from your cart");
                }
            }
        }

        private void Checkout()
        {
            printLogo.PrintLogo();
            Console.WriteLine("Do you wish to view your cart total in another currency before check out?:\n\tPress 1 to continue with Swedish Crowns\n\tPress 2 to convert to Euros(EUR)\n\tPress 3 to convert to Dollars(USD)");
            int userInput = Convert.ToInt32(Console.ReadLine());
            string currencyCode = "SEK";

            if (CurrentCustomer is GoldCustomer goldCustomer)
            {
                printLogo.PrintLogo();
                if (userInput == 1)
                {
                    currencyCode = "SEK";
                }
                else if (userInput == 2)
                {
                    currencyCode = "EUR";
                }
                else if (userInput == 3)
                {
                    currencyCode = "USD";
                }
                Console.WriteLine(goldCustomer.GoldCustomerSummary(CurrentCustomer, currencies, currencyCode));
            }
            else if (CurrentCustomer is SilverCustomer silverCustomer)
            {
                printLogo.PrintLogo();
                if (userInput == 1)
                {
                    currencyCode = "SEK";
                }
                else if (userInput == 2)
                {
                    currencyCode = "EUR";
                }
                else if (userInput == 3)
                {
                    currencyCode = "USD";
                }
                Console.WriteLine(silverCustomer.SilverCustomerSummary(CurrentCustomer, currencies, currencyCode));
            }
            else if (CurrentCustomer is BronzeCustomer bronzeCustomer)
            {
                printLogo.PrintLogo();
                if (userInput == 1)
                {
                    currencyCode = "SEK";
                }
                else if (userInput == 2)
                {
                    currencyCode = "EUR";
                }
                else if (userInput == 3)
                {
                    currencyCode = "USD";
                }
                Console.WriteLine(bronzeCustomer.BronzeCustomerSummary(CurrentCustomer, currencies, currencyCode));
            }
            else
            {
                printLogo.PrintLogo();
                if (userInput == 1)
                {
                    currencyCode = "SEK";
                }
                else if (userInput == 2)
                {
                    currencyCode = "EUR";
                }
                else if (userInput == 3)
                {
                    currencyCode = "USD";
                }
                Console.WriteLine($"\tYour total is: {Math.Round(CurrentCustomer.CartTotal(CurrentCustomer) / currencies.Currencies[currencyCode], 2)} {currencyCode}");
            }
            Console.WriteLine("\n\tThank you for your order\n\tPress 1 to log out\n\tPress 2 to exit shop");

            while (true)
            {
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    CurrentCustomer!.Cart.Clear();
                    CurrentCustomer = null!;
                    MainMenu();
                }
                else if (choice == "2")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid choice, choose between 1 to log out and 2 to exit shop");
                }
            }
        }
    }
}
