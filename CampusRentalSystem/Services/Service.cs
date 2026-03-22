using CampusRentalSystem.Models.Devices;
using CampusRentalSystem.Models.Rentals;
using CampusRentalSystem.Models.Users;

namespace CampusRentalSystem.Services;

public class Service
{
    private Dictionary<int, User> Users { get; } = [];
    private Dictionary<int, Device> Devices { get; } = [];
    private HashSet<Rental> Rentals { get; } = [];

    public void AddNewUser(User user)
    {
        if (!Users.TryAdd(user.Id, user))
        {
            Console.WriteLine($"[INFO]: User with Id={user.Id} is already added to the system.");
            return;
        }

        Console.WriteLine($"[INFO]: User with Id={user.Id} has been added to the system.");
    }

    public void AddNewDevice(Device device)
    {
        if (!Devices.TryAdd(device.Id, device))
        {
            Console.WriteLine($"[INFO]: Device with Id={device.Id} is already added to the system.");
            return;
        }

        Console.WriteLine($"[INFO]: Device with Id={device.Id} has been added to the system.");
    }

    public void ShowAllDevices()
    {
        var devices = Devices
            .Select(i => i.Value)
            .ToList();
        if (!devices.Any())
        {
            Console.WriteLine("[INFO]: No devices found in the system.");
            return;
        }

        Console.WriteLine("[INFO]: All devices:");
        devices.ForEach(i => Console.WriteLine($"\t{i}"));
    }

    public void ShowAvailableDevices()
    {
        var devices = Devices
            .Select(i => i.Value)
            .ToList();
        if (!devices.Any())
        {
            Console.WriteLine("[INFO]: No devices found in the system.");
            return;
        }

        var availableDevices = devices
            .Where(i => i.DeviceStatus == DeviceStatus.Available)
            .ToList();
        if (!availableDevices.Any())
        {
            Console.WriteLine("[INFO]: No available devices found at the moment.");
            return;
        }

        Console.WriteLine("[INFO]: All available devices:");
        availableDevices.ForEach(i => Console.WriteLine($"\t{i}"));
    }

    public void ShowUsersActiveRentals(User user)
    {
        var activeRentals = Rentals
            .Where(i => i.User.Equals(user) && i.ActualReturnDate is null)
            .ToList();

        if (activeRentals.Count == 0)
        {
            Console.WriteLine($"[INFO]: User {user.Id} has no active rentals.");
            return;
        }

        Console.WriteLine($"[INFO]: Active rentals of user {user.Id}:");
        activeRentals.ForEach(i => Console.WriteLine($"\t{i}"));
    }

    public void ShowOverdueRentals(DateOnly currentDate)
    {
        var overdueRentals = Rentals
            .Where(i => i.ActualReturnDate is null && i.ExpectedReturnDate < currentDate)
            .ToList();

        if (overdueRentals.Count == 0)
        {
            Console.WriteLine("[INFO]: No overdue rentals in the system.");
            return;
        }

        Console.WriteLine("[INFO]: Overdue rentals:");
        overdueRentals.ForEach(i => Console.WriteLine($"\t{i}"));
    }

    public void GenerateReport()
    {
        Console.WriteLine("[INFO]: Generated report:");
        Console.WriteLine($"\tTotal users: {Users.Count}");
        Console.WriteLine($"\tTotal devices: {Devices.Count}");
        Console.WriteLine(
            $"\tAvailable devices: {Devices.Values.Count(d => d.DeviceStatus == DeviceStatus.Available)}");
        Console.WriteLine($"\tDamaged devices: {Devices.Values.Count(d => d.DeviceStatus == DeviceStatus.Damaged)}");
        Console.WriteLine($"\tTotal active rentals: {Rentals.Count(r => r.ActualReturnDate is null)}");
    }

    public void RentDevice(int userId, int deviceId, DateOnly rentalDate, DateOnly expectedReturnDate)
    {
        if (!Users.TryGetValue(userId, out var user))
        {
            Console.WriteLine($"[INFO]: User with Id={userId} does not exist in the system.");
            return;
        }

        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[INFO]: Device with Id={deviceId} does not exist in the system.");
            return;
        }

        if (user.ActiveRentals >= user.MaxActiveRentals)
        {
            Console.WriteLine(
                $"[INFO]: User with Id={user.Id} has reached the rental limit ({user.MaxActiveRentals}).");
            return;
        }

        if (device.DeviceStatus != DeviceStatus.Available)
        {
            Console.WriteLine($"[ERROR]: Device with Id={device.Id} is unavailable for rental.");
            return;
        }

        Rentals.Add(new Rental(user, device, rentalDate, expectedReturnDate));
        device.DeviceStatus = DeviceStatus.Unavailable;
        user.ActiveRentals += 1;
        Console.WriteLine(
            $"[INFO]: User with Id={user.Id} rented device with Id={device.Id} from {rentalDate} to {expectedReturnDate}.");
    }

    public void ReturnDevice(int userId, int deviceId, DateOnly actualReturnDate)
    {
        if (!Users.TryGetValue(userId, out var user))
        {
            Console.WriteLine($"[ERROR]: User with Id={userId} does not exist in the system.");
            return;
        }

        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[ERROR]: Device with Id={deviceId} does not exist in the system.");
            return;
        }

        var rental = Rentals
            .FirstOrDefault(i => i.User.Id == userId && i.Device.Id == deviceId && i.ActualReturnDate is null);

        if (rental is null)
        {
            Console.WriteLine(
                $"[ERROR]: Active rental for user with Id={userId} and device with Id={deviceId} does not exist in the system.");
            return;
        }

        rental.ActualReturnDate = actualReturnDate;
        if (rental.ActualReturnDate > rental.ExpectedReturnDate)
        {
            var daysLate = rental.ActualReturnDate.Value.DayNumber - rental.ExpectedReturnDate.DayNumber;
            var penalty = daysLate * 5.0;
            Console.WriteLine($"[INFO]: Device returned late by {daysLate} days. Penalty to pay: {penalty} PLN.");
        }
        else
        {
            Console.WriteLine($"[INFO]: Device returned on time. No penalty.");
        }

        device.DeviceStatus = DeviceStatus.Available;
        user.ActiveRentals -= 1;
        Console.WriteLine($"[INFO]: User {userId} successfully returned device {deviceId}.");
    }

    public void SendToRepair(int deviceId)
    {
        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[INFO]: Device with Id={deviceId} does not exist in the system.");
            return;
        }

        if (device.DeviceStatus != DeviceStatus.Available)
        {
            Console.WriteLine($"[INFO]: Device with Id={deviceId} is not available and cannot be sent to repair.");
            return;
        }

        device.DeviceStatus = DeviceStatus.Damaged;
        Console.WriteLine($"[INFO]: Device with Id={deviceId} has been sent to repair.");
    }

    public void CollectFromRepair(int deviceId)
    {
        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[INFO]: Device with Id={deviceId} does not exist in the system.");
            return;
        }

        if (device.DeviceStatus != DeviceStatus.Damaged)
        {
            Console.WriteLine($"[INFO]: Device with Id={deviceId} is not a damaged device.");
            return;
        }

        device.DeviceStatus = DeviceStatus.Available;
        Console.WriteLine($"[INFO]: Device with Id={deviceId} has been collected from repair and is available.");
    }
}