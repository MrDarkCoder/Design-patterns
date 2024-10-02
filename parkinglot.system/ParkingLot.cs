public class ParkingTicket(Vehicle vehicle, ParkingSpot spot, int levelId)
{
    public Vehicle Vehicle { get; } = vehicle;
    public int LevelId { get; } = levelId;
    public int SpotId { get; } = spot.SpotId;
}


public class ParkingLot
{
    private List<ParkingLevel> _levels;

    public ParkingLot(List<ParkingLevel> levels)
    {
        _levels = levels;
    }

    public ParkingTicket ParkVehicle(Vehicle vehicle)
    {
        // linear search to find parking lot
        foreach (var level in _levels)
        {
            var availableSpot = level.FindAvailableSpot(vehicle);

            if (availableSpot != null)
            {
                availableSpot.ParkVehicle(vehicle);

                return new ParkingTicket(vehicle, availableSpot, level.LevelId);
            }
        }

        throw new InvalidOperationException("No available parking spots for this vehicle.");
    }

    public void ReleaseVehicle(ParkingTicket ticket)
    {
        var level = _levels.FirstOrDefault(l => l.LevelId == ticket.LevelId);
        level?.ReleaseSpot(ticket.SpotId);
    }
}

