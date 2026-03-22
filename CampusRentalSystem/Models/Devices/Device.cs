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

    protected int Id { get; set; }
    protected string Name { get; set; }
    protected string Brand { get; set; }
    protected double Weight { get; set; }
    protected DateOnly PurchaseDate { get; set; }
    protected DeviceStatus DeviceStatus { get; set; } = DeviceStatus.Available;
}