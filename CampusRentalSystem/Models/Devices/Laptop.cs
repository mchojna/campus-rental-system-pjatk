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
    public static DeviceType _deviceType = DeviceType.Laptop;

    public string ProcessorModel { get; set; } = processorModel;
    public string RamCapacity { get; set; } = ramCapacity;
    public string StorageCapacity { get; set; } = storageCapacity;
    public string OperatingSystem { get; set; } = operatingSystem;
}