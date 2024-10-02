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
