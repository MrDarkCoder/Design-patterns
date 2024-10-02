
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
