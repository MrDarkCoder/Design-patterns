
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

void SimulateCustomerTransaction(VendingMachine vendingMachine, string productName, decimal[] moneyToInsert)
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
