using System;
using System.Collections.Generic;

public class Address
{
    private string StreetAddress { get; set; }
    private string City { get; set; }
    private string StateProvince { get; set; }
    private string Country { get; set; }

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        StateProvince = stateProvince;
        Country = country;
    }

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{StreetAddress}\n{City}, {StateProvince}\n{Country}";
    }
}

public class Customer
{
    private string Name { get; set; }
    private Address Address { get; set; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }

    public string GetName()
    {
        return Name;
    }

    public string GetAddress()
    {
        return Address.GetFullAddress();
    }
}

public class Product
{
    private string Name { get; set; }
    private string ProductId { get; set; }
    private decimal Price { get; set; }
    private int Quantity { get; set; }

    public Product(string name, string productId, decimal price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public decimal GetTotalCost()
    {
        return Price * Quantity;
    }

    public string GetName()
    {
        return Name;
    }

    public string GetProductId()
    {
        return ProductId;
    }
}

public class Order
{
    private List<Product> Products { get; set; }
    private Customer Customer { get; set; }

    public Order(Customer customer)
    {
        Products = new List<Product>();
        Customer = customer;
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public decimal GetTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in Products)
        {
            totalCost += product.GetTotalCost();
        }
        decimal shippingCost = Customer.IsInUSA() ? 5 : 35;
        return totalCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (var product in Products)
        {
            label += $"{product.GetName()} ({product.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"{Customer.GetName()}\n{Customer.GetAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Elm St", "London", "", "UK");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create products
        Product product1 = new Product("Product A", "A123", 10.99m, 2);
        Product product2 = new Product("Product B", "B456", 9.99m, 3);
        Product product3 = new Product("Product C", "C789", 12.99m, 1);
        Product product4 = new Product("Product D", "D012", 8.99m, 4);

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product3);
        order2.AddProduct(product4);

        // Display order information
        Console.WriteLine("Order 1:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}");
        Console.WriteLine();

        Console.WriteLine("Order 2:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}");
    }
}

