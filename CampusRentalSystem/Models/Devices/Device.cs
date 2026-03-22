namespace CampusRentalSystem.Models.Devices;

public class Device
{
    private static int _globalId = 0;

    protected Device(string name, string brand, double weight, DateOnly purchaseDate)
    {
        Id = _globalId++;

        Name = name;
        Brand = brand;
        Weight = weight;
        PurchaseDate = purchaseDate;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Weight { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public DeviceStatus DeviceStatus { get; set; } = DeviceStatus.Available;
}