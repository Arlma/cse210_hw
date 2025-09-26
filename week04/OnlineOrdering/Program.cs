using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("45 Queen St", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("Alice Johnson", address1);
        Customer customer2 = new Customer("Bob Smith", address2);

        // Create orders
        Order order1 =  new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P1001", 1200.50, 1));
        order1.AddProduct(new Product("Mouse", "P1002", 25.75, 2));

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Phone", "P2001", 800.00, 1));
        order2.AddProduct(new Product("Headphones", "P2002", 150.00, 1));
        order2.AddProduct(new Product("Charger", "P2003", 20.00, 2));

        // Display order 1
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalCost()}\n");

        // Display order 2
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalCost()}");
    }
}
