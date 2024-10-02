public class ParkingSpot
{
    public int SpotId { get; set; }
    public VehicleType VehicleType { get; set; }

    public bool IsOccupied { get; set; }
    public Vehicle? OccupiedBy { get; set; }


    public ParkingSpot(int spotId, VehicleType vehicleType, bool isOccupied = false)
    {
        SpotId = spotId;
        VehicleType = vehicleType;
        IsOccupied = isOccupied;
    }

    public bool CanFitVehicle(Vehicle vehicle)
    {
        return vehicle.VehicleType <= VehicleType;
    }

    public void ParkVehicle(Vehicle vehicle)
    {
        if (!CanFitVehicle(vehicle)) throw new InvalidOperationException("Vehicle cannot fit in this spot.");

        if (IsOccupied) throw new InvalidOperationException("Already occupied");

        OccupiedBy = vehicle;
        IsOccupied = true;
    }

    public void ReleaseVehicle()
    {
        OccupiedBy = null;
        IsOccupied = !IsOccupied;
    }
}

