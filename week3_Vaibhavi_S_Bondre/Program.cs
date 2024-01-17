using System;
using System.Collections.Generic;
using System.Linq; 
class Item
{
    public int ID { get; private set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public Item(int id, string name, double price, int quantity)
    {
        ID = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}
class Inventory
{
    private List<Item> items = new List<Item>();
    public void AddItem(Item item)
    {
        items.Add(item);
    }
    public void DisplayAllItems()
    {
        Console.WriteLine("Inventory Items:");
        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.ID}, Name: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
        }
    }
    public Item FindItemByID(int id)
    {
        return items.Find(item => item.ID == id);
    }
    public void UpdateItem(Item updatedItem)
    {
        Item existingItem = FindItemByID(updatedItem.ID);
        if (existingItem != null)
        {
            existingItem.Name = updatedItem.Name;
            existingItem.Price = updatedItem.Price;
            existingItem.Quantity = updatedItem.Quantity;
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
    public void DeleteItem(int id)
    {
        Item itemToDelete = FindItemByID(id);
        if (itemToDelete != null)
        {
            items.Remove(itemToDelete);
            Console.WriteLine("Item deleted successfully.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
    public int GenerateNextItemID()
    {
        int maxID = items.Count > 0 ? items.Max(item => item.ID) : 0;
        return maxID + 1;
    }
}
class Program
{
    static void Main()
    {
        Inventory inventory = new Inventory();
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add a new item");
            Console.WriteLine("2. Display all items");
            Console.WriteLine("3. Find an item by ID");
            Console.WriteLine("4. Update an item's information");
            Console.WriteLine("5. Delete an item");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":   int newItemID = inventory.GenerateNextItemID();
                            Console.Write("Enter item name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter item price: ");
                            double newPrice = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter item quantity: ");
                            int newQuantity = Convert.ToInt32(Console.ReadLine());
            
                            Item newItem = new Item(newItemID, newName, newPrice, newQuantity);
                            inventory.AddItem(newItem);
                            Console.WriteLine("Item added successfully.");
                            break;
                    

                case "2":   inventory.DisplayAllItems();
                            break;
                    

                case "3":   Console.Write("Enter item ID to find: ");
                            int searchID = Convert.ToInt32(Console.ReadLine());
                            Item foundItem = inventory.FindItemByID(searchID);
                            if (foundItem != null)
                            {
                                Console.WriteLine($"Item found - ID: {foundItem.ID}, Name: {foundItem.Name}, Price: {foundItem.Price}, Quantity: {foundItem.Quantity}");
                            }
                            else
                            {
                                Console.WriteLine("Item not found.");
                            }
                            break;
                    

                case "4":   Console.Write("Enter item ID to update: ");
                            int updateID = Convert.ToInt32(Console.ReadLine());
            
                            Item updatedItem = inventory.FindItemByID(updateID);
                            if (updatedItem != null)
                            {
                                Console.Write("Enter new item name: ");
                                updatedItem.Name = Console.ReadLine();
                                Console.Write("Enter new item price: ");
                                updatedItem.Price = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Enter new item quantity: ");
                                updatedItem.Quantity = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Item updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Item not found.");
                            }
                            break;
                    

                case "5":   Console.Write("Enter item ID to delete: ");
                            int deleteID = Convert.ToInt32(Console.ReadLine());
                            inventory.DeleteItem(deleteID);
                            break;
                    

                case "6":   Console.WriteLine("Exiting program. Goodbye!");
                            Environment.Exit(0);
                            break;
                    

                default:    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;

            }
        }
    }
}
