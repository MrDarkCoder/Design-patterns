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
