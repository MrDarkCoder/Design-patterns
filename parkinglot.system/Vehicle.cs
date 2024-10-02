public enum VehicleType
{
    Motorcycle,
    Car,
    Truck
}

public abstract class Vehicle(string licensePlate, VehicleType vehicleType)
{
    public string LicensePlate { get; set; } = licensePlate;
    public VehicleType VehicleType { get; set; } = vehicleType;
}

public class Car(string licensePlate) : Vehicle(licensePlate, VehicleType.Car) { }
public class Motorcycle(string licensePlate) : Vehicle(licensePlate, VehicleType.Motorcycle) { }
public class Truck(string licensePlate) : Vehicle(licensePlate, VehicleType.Truck) { }
