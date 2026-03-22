namespace CampusRentalSystem.Models.Devices;

public class Device
{
    private static int _globalId = 0;

    protected Device(string name, string brand, double weight, DateOnly purchaseDate, DeviceType deviceType)
    {
        Id = _globalId++;

        Name = name;
        Brand = brand;
        Weight = weight;
        PurchaseDate = purchaseDate;
        DeviceType = deviceType;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (GetType() != obj.GetType()) return false;

        var other = (Device)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        return
            $"Device (Id={Id}, Name={Name}, Brand={Brand}, Weight={Weight}, PurchaseDate={PurchaseDate}, DeviceStatus={DeviceStatus})";
    }

    public readonly int Id;
    protected readonly string Name;
    protected readonly string Brand;
    protected readonly double Weight;
    protected readonly DateOnly PurchaseDate;
    protected readonly DeviceType DeviceType;
    public DeviceStatus DeviceStatus { get; set; } = DeviceStatus.Available;
}