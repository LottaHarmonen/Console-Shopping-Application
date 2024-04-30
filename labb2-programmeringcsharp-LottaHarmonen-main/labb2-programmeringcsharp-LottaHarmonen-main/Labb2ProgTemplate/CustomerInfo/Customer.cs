using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb2ProgTemplate.Models;

namespace Labb2ProgTemplate.CustomerInfo
{
    public class Customer
    {
        public string Name { get; private set; }
        private string Password { get; set; }

        private List<Product> _cart;
        public List<Product> Cart { get { return _cart; } }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            _cart = new List<Product>();
        }

        public override string ToString()
        {
            string summary = string.Empty;
            summary += $"Username:{Name}\n";
            summary += $"Password:{Password}\n";
            summary += $"Membership:{GetType().Name}";

            return summary;
        }


        public bool CheckPassword(string password, Customer customer)
        {
            if (password == customer.Password)
            {
                return true;
            }
            return false;
        }

        public void AddToCart(Product product)
        {
            _cart.Add(product);
            Console.WriteLine(product.Name + " added to your cart! ");
            Console.WriteLine();
        }

        public void RemoveFromCart(Product product)
        {
            _cart.Remove(product);
            Console.WriteLine(product.Name + " removed from your cart!\nPress enter to continue shopping");
            
        }

        public double CartTotal(Customer currentCustomer)
        {
            double total = 0;
            foreach (Product product in currentCustomer._cart)
            {
                total += product.Price;
            }
            return total;
        }
    }
}
