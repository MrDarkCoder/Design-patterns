using System.Collections.Concurrent;
/**
ConcurrentDictionary<string, Product>: A thread-safe dictionary that stores products using their name as the key.
AddProduct(): Adds a product to the inventory. If the product already exists, it fails to add.
RestockProduct(): Restocks a product by increasing its quantity.
IsProductAvailable(): Checks if a product is in stock.
GetProduct(): Retrieves the product details.
PurchaseProduct(): Purchases the product by reducing its quantity if available.
DisplayInventory(): Displays the current list of products with their prices and quantities.
 */
public class Inventory
{
    private ConcurrentDictionary<string, Product> _products;

    public Inventory()
    {
        _products = new();
    }

    public bool AddProduct(Product product)
    {
        return _products.TryAdd(product.Name, product);
    }

    public bool RemoveProduct(Product product)
    {
        return _products.TryRemove(new KeyValuePair<string, Product>(product.Name, product));
    }

    public bool RestockProduct(string productName, int quantity)
    {
        if (_products.TryGetValue(productName, out Product? product))
        {
            product?.Restock(quantity);
            return true;
        }

        return false;
    }

    public bool IsProductAvailable(string productName)
    {
        return _products.TryGetValue(productName, out Product? product) && product.IsAvailable();
    }

    public Product? GetProduct(string productName)
    {
        _products.TryGetValue(productName, out var product);
        return product;
    }

    public bool PurchaseProduct(string productName)
    {
        if (_products.TryGetValue(productName, out Product? product) && product is not null && product.IsAvailable())
        {
            product.ReduceQuantity();
            return true;
        }

        return false;
    }

    public void DisplayInventory()
    {
        Console.WriteLine("\nCurrent Inventory:");

        foreach (var kvp in _products)
        {
            Console.WriteLine($"{kvp.Value.Name} - Price: {kvp.Value.Price}, Quantity: {kvp.Value.Quantity}");
        }
    }

}
