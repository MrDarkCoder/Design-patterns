public class ParkingSystem
{
    private ParkingLot parkingLot;

    public ParkingSystem(ParkingLot lot)
    {
        parkingLot = lot;
    }

    public ParkingTicket Enter(Vehicle vehicle)
    {
        return parkingLot.ParkVehicle(vehicle);
    }

    public void Exit(ParkingTicket ticket)
    {
        parkingLot.ReleaseVehicle(ticket);
    }
}

