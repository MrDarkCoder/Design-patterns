
/**
Requirements
The parking lot should have multiple levels, each level with a certain number of parking spots.
The parking lot should support different types of vehicles, such as cars, motorcycles, and trucks.
Each parking spot should be able to accommodate a specific type of vehicle.
The system should assign a parking spot to a vehicle upon entry and release it when the vehicle exits.
The system should track the availability of parking spots and provide real-time information to customers.
The system should handle multiple entry and exit points and support concurrent access.
 */

// Create parking spots for a level
List<ParkingSpot> level1Spots =
        [
            new(1, VehicleType.Motorcycle),
            new(2, VehicleType.Car),
            new(3, VehicleType.Car),
            new(4, VehicleType.Truck)
        ];

List<ParkingSpot> level2Spots =
        [
            new(5, VehicleType.Motorcycle),
            new(6, VehicleType.Car),
            new(7, VehicleType.Truck)
        ];

// Create parking levels
ParkingLevel level1 = new(1, level1Spots);
ParkingLevel level2 = new(2, level2Spots);

// Create the parking lot with levels
List<ParkingLevel> levels = [level1, level2];

ParkingLot parkingLot = new(levels);

// Create the parking system
ParkingSystem parkingSystem = new(parkingLot);

// Simulate vehicles entering
Vehicle car = new Car("CAR123");
Vehicle motorcycle = new Motorcycle("MOTO123");
Vehicle truck = new Truck("TRUCK123");

Console.WriteLine("Vehicle entry:");

// Park the car
var carTicket = parkingSystem.Enter(car);
Console.WriteLine($"Car parked at level {carTicket.LevelId}, spot {carTicket.SpotId}");

// Park the motorcycle
var motorcycleTicket = parkingSystem.Enter(motorcycle);
Console.WriteLine($"Motorcycle parked at level {motorcycleTicket.LevelId}, spot {motorcycleTicket.SpotId}");

// Park the truck
var truckTicket = parkingSystem.Enter(truck);
Console.WriteLine($"Truck parked at level {truckTicket.LevelId}, spot {truckTicket.SpotId}");

// Simulate vehicle exits
Console.WriteLine("Vehicle exit:");

// Exit the car
parkingSystem.Exit(carTicket);
Console.WriteLine("Car has exited.");

// Exit the motorcycle
parkingSystem.Exit(motorcycleTicket);
Console.WriteLine("Motorcycle has exited.");

// Exit the truck
parkingSystem.Exit(truckTicket);
Console.WriteLine("Truck has exited.");


/**
 using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        // Create parking spots for a level
        List<ParkingSpot> level1Spots = new List<ParkingSpot>
        {
            new ParkingSpot(1, VehicleSize.Motorcycle),
            new ParkingSpot(2, VehicleSize.Car),
            new ParkingSpot(3, VehicleSize.Car),
            new ParkingSpot(4, VehicleSize.Truck)
        };

        List<ParkingSpot> level2Spots = new List<ParkingSpot>
        {
            new ParkingSpot(5, VehicleSize.Motorcycle),
            new ParkingSpot(6, VehicleSize.Car),
            new ParkingSpot(7, VehicleSize.Truck)
        };

        // Create parking levels
        ParkingLevel level1 = new ParkingLevel(1, level1Spots);
        ParkingLevel level2 = new ParkingLevel(2, level2Spots);

        // Create the parking lot with levels
        List<ParkingLevel> levels = new List<ParkingLevel> { level1, level2 };
        ParkingLot parkingLot = new ParkingLot(levels);

        // Create the parking system
        ParkingSystem parkingSystem = new ParkingSystem(parkingLot);

        // Create tasks to simulate concurrent parking and exit
        var tasks = new List<Task>
        {
            Task.Run(() => SimulateVehicleEntryExit(parkingSystem, new Car("CAR123"), "Car")),
            Task.Run(() => SimulateVehicleEntryExit(parkingSystem, new Motorcycle("MOTO123"), "Motorcycle")),
            Task.Run(() => SimulateVehicleEntryExit(parkingSystem, new Truck("TRUCK123"), "Truck")),
            Task.Run(() => SimulateVehicleEntryExit(parkingSystem, new Car("CAR456"), "Car 2")),
            Task.Run(() => SimulateVehicleEntryExit(parkingSystem, new Motorcycle("MOTO456"), "Motorcycle 2"))
        };

        // Wait for all tasks to complete
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("\nAll vehicles have entered and exited the parking lot.");
    }

    static void SimulateVehicleEntryExit(ParkingSystem parkingSystem, Vehicle vehicle, string vehicleType)
    {
        try
        {
            Console.WriteLine($"{vehicleType} attempting to park...");
            var ticket = parkingSystem.Enter(vehicle);
            Console.WriteLine($"{vehicleType} parked at level {ticket.LevelId}, spot {ticket.SpotId}");

            // Simulate some time parked
            Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5))).Wait();

            Console.WriteLine($"{vehicleType} exiting...");
            parkingSystem.Exit(ticket);
            Console.WriteLine($"{vehicleType} has exited.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{vehicleType} encountered an issue: {ex.Message}");
        }
    }
}

 */