namespace CampusRentalSystem.Models.Devices;

public class Laptop(
    string name,
    string brand,
    double weight,
    DateOnly purchaseDate,
    string processorModel,
    string ramCapacity,
    string storageCapacity,
    string operatingSystem)
    : Device(name, brand, weight, purchaseDate)
{
    private static DeviceType _deviceType = DeviceType.Laptop;

    protected string ProcessorModel { get; set; } = processorModel;
    protected string RamCapacity { get; set; } = ramCapacity;
    protected string StorageCapacity { get; set; } = storageCapacity;
    protected string OperatingSystem { get; set; } = operatingSystem;
}