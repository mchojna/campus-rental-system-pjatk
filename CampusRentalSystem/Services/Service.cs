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
            Console.WriteLine($"[ERROR]: User with Id={user.Id} is already added.");
            return;
        }
        Console.WriteLine($"[INFO]: User with Id={user.Id} has been added.");
    }

    public void AddNewDevice(Device device)
    {
        if (!Devices.TryAdd(device.Id, device))
        {
            Console.WriteLine($"[ERROR]: Device with Id={device.Id} is already added.");
            return;
        }
        Console.WriteLine($"[INFO]: Device with Id={device.Id} has been added.");
    }

    public void ShowAllDevice()
    {
        var devices = Devices
            .Select(i => i.Value)
            .ToList();
        if (!devices.Any())
        {
            Console.WriteLine("[ERROR] ...");
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
            Console.WriteLine("[ERROR] ...");
            return;
        }
        var availableDevices = devices
            .Where(i => i.DeviceStatus == DeviceStatus.Available)
            .ToList();
        if (!availableDevices.Any())
        {
            Console.WriteLine("[ERROR] ...");
            return;
        }
        Console.WriteLine("[INFO]: All available devices:");
        devices.ForEach(i => Console.WriteLine($"\t{i}"));
    }
    
    public void ShowUsersActiveRentals(User user)
    {
        Console.WriteLine($"[INFO]: Active rentals of user {user}:");
        Rentals.ToList()
            .Where(i => i.User.Equals(user) && i.Device.DeviceStatus==DeviceStatus.Unavailable)
            .ToList()
            .ForEach(i => Console.WriteLine($"\t{i}"));
        // a co jak tu nic nie ma?
    }
    
    public void ShowOverdueRentals()
    {
        Console.WriteLine("[INFO]: Overdue rentals:");
        // Rentals.ToList()
        //     .Where(i => i.)
        //     .ToList()
        //     .ForEach(i => Console.WriteLine($"\t{i}"));
        // a co jak tu nic nie ma?
    }
    
    public void GenerateReport()
    {
        // TODO
    }

    public void RentDevice(int userId, int deviceId, DateOnly rentalDate, DateOnly expectedReturnDate)
    {
        if (!Users.TryGetValue(userId, out var user))
        {
            Console.WriteLine($"[ERROR]: User with Id={userId} does not exist in system.");
            return;
        }

        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[ERROR]: Device with Id={deviceId} does not exist in system.");
            return;
        }

        if (user.ActiveRentals >= user.MaxActiveRentals)
        {
            Console.WriteLine($"[ERROR]: User with Id={user.Id} has reached the rental limit ({user.MaxActiveRentals}).");
            return;
        }

        if (device.DeviceStatus != DeviceStatus.Available)
        {
            Console.WriteLine($"[ERROR]: Device with Id={device.Id} is unavailable for renal.");
            return;
        }
        
        Rentals.Add(new Rental(user, device, rentalDate, expectedReturnDate));
        device.DeviceStatus = DeviceStatus.Unavailable;
        user.ActiveRentals += 1;
        Console.WriteLine($"[INFO]: User with Id={user.Id} rented device with Id={device.Id} from {rentalDate} to {expectedReturnDate}.");
    }

    public void ReturnDevice(int userId, int deviceId, DateOnly actualReturnDate)
    {
        if (!Users.TryGetValue(userId, out var user))
        {
            Console.WriteLine($"[ERROR]: User with Id={userId} does not exist in system.");
            return;
        }

        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[ERROR]: Device with Id={deviceId} does not exist in system.");
            return;
        }

        Rental? rental = Rentals
            .ToList()
            .FirstOrDefault(i => i.User.Id == userId && i.Device.Id == deviceId && i.ActualReturnDate is not null, null);

        if (rental is null)
        {
            Console.WriteLine($"[ERROR]: Rental with user with Id={userId} and device with Id={deviceId} does not exist in system.");
            return;
        }
        
        rental.ActualReturnDate = actualReturnDate;
        if (rental.ActualReturnDate > rental.ExpectedReturnDate)
        {
            // TODO: calculate penalty
            Console.WriteLine($"[INFO]: ...");
        }
        
        device.DeviceStatus = DeviceStatus.Available;
        user.ActiveRentals -= 1;
        Console.WriteLine($"[INFO]: ...");
    }
    
    public void SendToRepair(int deviceId)
    {
        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[ERROR]: ...");
            return;
        }

        if (device.DeviceStatus != DeviceStatus.Available)
        {
            Console.WriteLine($"[ERROR]: ...");
            return;
        }

        device.DeviceStatus = DeviceStatus.Damaged;
        Console.WriteLine($"[INFO]: ...");
    }

    public void CollectFromRepair(int deviceId)
    {
        if (!Devices.TryGetValue(deviceId, out var device))
        {
            Console.WriteLine($"[ERROR]: ...");
            return;
        }
        
        if (device.DeviceStatus != DeviceStatus.Damaged)
        {
            Console.WriteLine($"[ERROR]: ...");
            return;
        }
        
        device.DeviceStatus = DeviceStatus.Available;
        Console.WriteLine($"[INFO]: ...");
    }
}