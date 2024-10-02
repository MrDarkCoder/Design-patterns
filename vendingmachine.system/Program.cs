using System.Collections.Concurrent;


/**
 Requirements
The vending machine should support multiple products with different prices and quantities.
The machine should accept coins and notes of different denominations.
The machine should dispense the selected product and return change if necessary.
The machine should keep track of the available products and their quantities.
The machine should handle multiple transactions concurrently and ensure data consistency.
The machine should provide an interface for restocking products and collecting money.
The machine should handle exceptional scenarios, such as insufficient funds or out-of-stock products.
 */



/*
Name: The name of the product (e.g., "Soda").
Price: The price of the product.
Quantity: The current stock available in the machine.
IsAvailable(): Returns true if the product is available (i.e., quantity > 0).
ReduceQuantity(): Reduces the quantity when a product is purchased.
Restock(): Increases the quantity when restocking.
 */
public class Product(string name, decimal price, int quantity)
{
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int Quantity { get; set; } = quantity;

    public bool IsAvailable()
    {
        return Quantity > 0;
    }

    public void ReduceQuantity(int quantity = 1)
    {
        if (Quantity > 0 && Quantity >= quantity) Quantity -= quantity;
        else throw new InvalidOperationException("Insufficient quantity to reduce");
    }

    public void Restock(int quantity)
    {
        Quantity += quantity;
    }
}

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

/*
acceptedDenominations: A list of valid denominations (coins/notes) that the vending machine accepts.
InsertMoney(): Inserts money into the machine if the denomination is valid.
GetTotalInsertedAmount(): Returns the total money inserted by the user.
ResetInsertedAmount(): Resets the inserted amount after the transaction.
CalculateChange(): Calculates the change to return to the user.
 */
public class Currency
{
    private decimal _insertedAmount;
    private readonly List<decimal> _acceptedDenominations = [0.25m, 0.50m, 1.00m, 5.00m, 10.00m];

    public Currency()
    {
        _insertedAmount = 0m;
    }

    public bool InsertMoney(decimal amount)
    {
        if (_acceptedDenominations.Contains(amount))
        {
            _insertedAmount += amount;
            return true;
        }
        /*
            decimal originalAmount, newAmount;
            do
            {
                originalAmount = _insertedAmount;
                newAmount = originalAmount + amount;
            } 
            while (Interlocked.CompareExchange(ref _insertedAmount, newAmount, originalAmount) != originalAmount);
            
         */

        return false;
    }

    public decimal GetTotalInsertedAmount()
    {
        return _insertedAmount;
    }

    public void ResetInsertedAmount()
    {
        _insertedAmount = 0m;
    }

    public decimal CalculateChange(decimal productPrice)
    {
        decimal change = _insertedAmount - productPrice;

        if (change < 0)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        return change;
    }
}



public class VendingMachine
{
    private Inventory _inventory;
    private Currency _currency;

    public VendingMachine(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void StartTransaction()
    {
        _currency = new Currency();
        Console.WriteLine("Welcome to the Vending Machine!");
        _inventory.DisplayInventory();
    }

    public bool SelectProduct(string productName)
    {
        if (_inventory.IsProductAvailable(productName))
        {
            var product = _inventory.GetProduct(productName);

            if (product is null) return false;

            Console.WriteLine($"You selected: {product.Name}. Price: {product.Price}");

            return true;
        }

        Console.WriteLine("The selected product is unavailable");
        return false;
    }

    public void InsertMoney(decimal amount)
    {
        if (_currency.InsertMoney(amount))
        {
            Console.WriteLine($"Inserted: {amount:C}. Total inserted: {_currency.GetTotalInsertedAmount():C}");
        }
        else
        {
            Console.WriteLine("Invalid denomination. Please insert valid coins or notes.");
        }
    }

    public bool DispenseProduct(string productName)
    {
        var product = _inventory.GetProduct(productName);

        // Check if enough money has been inserted
        if (_currency.GetTotalInsertedAmount() >= product!.Price)
        {
            // Dispense product
            _inventory.PurchaseProduct(productName);
            Console.WriteLine($"Dispensing {product.Name}...");

            // Calculate and return change
            decimal change = _currency.CalculateChange(product.Price);
            Console.WriteLine($"Your change: {change:C}");

            // Reset inserted money
            _currency.ResetInsertedAmount();

            return true;
        }
        else
        {
            decimal requiredAmount = product!.Price - _currency.GetTotalInsertedAmount();
            Console.WriteLine($"Insufficient funds. Please insert an additional {requiredAmount:C}.");
            return false;
        }
    }

    public void CancelTransaction()
    {
        decimal totalMoneyInserted = _currency.GetTotalInsertedAmount();

        if (totalMoneyInserted > 0)
        {
            Console.WriteLine($"Transaction canceled. Returning money: {totalMoneyInserted:C}");
            _currency.ResetInsertedAmount();
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Create inventory and add products
        Inventory inventory = new Inventory();
        inventory.AddProduct(new Product("Soda", 1.50m, 10));
        inventory.AddProduct(new Product("Chips", 2.00m, 5));
        inventory.AddProduct(new Product("Candy", 1.00m, 8));

        // Create the vending machine
        VendingMachine vendingMachine = new VendingMachine(inventory);

        // Simulate concurrent transactions
        Task[] tasks =
        [
            Task.Run(() => SimulateCustomerTransaction(vendingMachine, "Soda", [1.00m, 0.50m])),
            Task.Run(() => SimulateCustomerTransaction(vendingMachine, "Chips", [2.00m])),
            Task.Run(() => SimulateCustomerTransaction(vendingMachine, "Candy", [1.00m])),
            Task.Run(() => SimulateCustomerTransaction(vendingMachine, "Soda", [0.50m, 1.00m])),
        ];

        Task.WaitAll(tasks);
        Console.WriteLine("\nAll transactions completed.");
    }

    static void SimulateCustomerTransaction(VendingMachine vendingMachine, string productName, decimal[] moneyToInsert)
    {
        vendingMachine.StartTransaction();

        if (vendingMachine.SelectProduct(productName))
        {
            foreach (var amount in moneyToInsert)
            {
                vendingMachine.InsertMoney(amount);
            }

            if (vendingMachine.DispenseProduct(productName))
            {
                Console.WriteLine($"{productName} was successfully purchased.\n");
            }
        }
        else
        {
            Console.WriteLine($"{productName} could not be selected.\n");
        }
    }
}