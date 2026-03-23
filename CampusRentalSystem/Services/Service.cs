using CampusRentalSystem.Exceptions;
using CampusRentalSystem.Models.Devices;
using CampusRentalSystem.Models.Rentals;
using CampusRentalSystem.Models.Users;

namespace CampusRentalSystem.Services;

public class Service(PenaltyCalculator penaltyCalculator)
{
    private Dictionary<int, User> Users { get; } = [];
    private Dictionary<int, Device> Devices { get; } = [];
    private HashSet<Rental> Rentals { get; } = [];

    public void AddNewUser(User user)
    {
        if (!Users.TryAdd(user.Id, user))
        {
            throw new AlreadyExistsException($"User with Id={user.Id} already exists.");
        }
    }

    public void AddNewDevice(Device device)
    {
        if (!Devices.TryAdd(device.Id, device))
        {
            throw new AlreadyExistsException($"Device with Id={device.Id} already exists.");
        }
    }

    public IReadOnlyList<Device> GetAllDevices()
    {
        return Devices.Values.ToList();
    }

    public IReadOnlyList<Device> GetAvailableDevices()
    {
        return Devices.Values
            .Where(i => i.DeviceStatus == DeviceStatus.Available)
            .ToList();
    }

    public IReadOnlyList<Rental> GetUsersActiveRentals(int userId, DateOnly currentDate)
    {
        if (!Users.ContainsKey(userId))
        {
            throw new NotFoundException($"User with Id={userId} does not exist.");
        }

        RefreshOpenRentalStatuses(currentDate);

        return Rentals
            .Where(i => i.User.Id == userId && i.Status != RentalStatus.Returned)
            .ToList();
    }

    public IReadOnlyList<Rental> GetOverdueRentals(DateOnly currentDate)
    {
        RefreshOpenRentalStatuses(currentDate);

        return Rentals
            .Where(i => i.Status == RentalStatus.Overdue)
            .ToList();
    }

    public (int TotalUsers, int TotalDevices, int AvailableDevices, int DamagedDevices, int TotalActiveRentals) GenerateReport()
    {
        return (
            TotalUsers: Users.Count,
            TotalDevices: Devices.Count,
            AvailableDevices: Devices.Values.Count(d => d.DeviceStatus == DeviceStatus.Available),
            DamagedDevices: Devices.Values.Count(d => d.DeviceStatus == DeviceStatus.Damaged),
            TotalActiveRentals: Rentals.Count(r => r.Status != RentalStatus.Returned)
        );
    }

    public Rental RentDevice(int userId, int deviceId, DateOnly rentalDate, DateOnly expectedReturnDate)
    {
        if (!Users.TryGetValue(userId, out var user))
        {
            throw new NotFoundException($"User with Id={userId} does not exist.");
        }

        if (!Devices.TryGetValue(deviceId, out var device))
        {
            throw new NotFoundException($"Device with Id={deviceId} does not exist.");
        }

        if (user.ActiveRentals >= user.MaxActiveRentals)
        {
            throw new RentalLimitExceededException($"User with Id={user.Id} reached rental limit ({user.MaxActiveRentals}).");
        }

        if (device.DeviceStatus != DeviceStatus.Available)
        {
            throw new DeviceUnavailableException($"Device with Id={device.Id} is unavailable for rental.");
        }

        var rental = new Rental(user, device, rentalDate, expectedReturnDate);
        Rentals.Add(rental);
        device.DeviceStatus = DeviceStatus.Unavailable;
        user.ActiveRentals += 1;

        return rental;
    }

    public decimal ReturnDevice(int userId, int deviceId, DateOnly actualReturnDate)
    {
        if (!Users.TryGetValue(userId, out var user))
        {
            throw new NotFoundException($"User with Id={userId} does not exist.");
        }

        if (!Devices.TryGetValue(deviceId, out var device))
        {
            throw new NotFoundException($"Device with Id={deviceId} does not exist.");
        }

        var rental = Rentals
            .FirstOrDefault(i => i.User.Id == userId && i.Device.Id == deviceId && i.Status != RentalStatus.Returned);

        if (rental is null)
        {
            throw new NotFoundException($"Active rental for user with Id={userId} and device with Id={deviceId} does not exist.");
        }
        
        var penalty = penaltyCalculator.CalculatePenalty(rental.ExpectedReturnDate, actualReturnDate);
        rental.MarkReturned(actualReturnDate, penalty);

        device.DeviceStatus = DeviceStatus.Available;
        user.ActiveRentals -= 1;

        return penalty;
    }

    public void SendToRepair(int deviceId)
    {
        if (!Devices.TryGetValue(deviceId, out var device))
        {
            throw new NotFoundException($"Device with Id={deviceId} does not exist.");
        }

        if (device.DeviceStatus != DeviceStatus.Available)
        {
            throw new InvalidDeviceStateException($"Device with Id={deviceId} is not available and cannot be sent to repair.");
        }
        
        device.DeviceStatus = DeviceStatus.Damaged;
    }

    public void CollectFromRepair(int deviceId)
    {
        if (!Devices.TryGetValue(deviceId, out var device))
        {
            throw new NotFoundException($"Device with Id={deviceId} does not exist.");
        }

        if (device.DeviceStatus != DeviceStatus.Damaged)
        {
            throw new InvalidDeviceStateException($"Device with Id={deviceId} is not a damaged device.");
        }

        device.DeviceStatus = DeviceStatus.Available;
    }

    private void RefreshOpenRentalStatuses(DateOnly currentDate)
    {
        foreach (var rental in Rentals.Where(r => r.Status != RentalStatus.Returned))
        {
            rental.Status = currentDate > rental.ExpectedReturnDate ? RentalStatus.Overdue : RentalStatus.Active;
        }
    }
}