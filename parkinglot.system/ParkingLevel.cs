public class ParkingLevel
{
    public int LevelId { get; }

    private readonly List<ParkingSpot> _spots;

    public ParkingLevel(int levelId, List<ParkingSpot> spots)
    {
        LevelId = levelId;
        _spots = spots;
    }

    public ParkingSpot? FindAvailableSpot(Vehicle vehicle)
    {
        return _spots.FirstOrDefault(spot => spot.CanFitVehicle(vehicle) && !spot.IsOccupied);
    }

    public void ReleaseSpot(int spotId)
    {
        var spot = _spots.FirstOrDefault(s => s.SpotId == spotId);

        if (spot != null && spot.IsOccupied)
        {
            spot.ReleaseVehicle();
        }
    }
}
