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
    : Device(name, brand, weight, purchaseDate, DeviceType.Laptop)
{
    public override string ToString()
    {
        return
            $"Laptop (Id={Id}, Name={Name}, Brand={Brand}, Weight={Weight}, PurchaseDate={PurchaseDate}, DeviceStatus={DeviceStatus}, " +
            $"DeviceType={DeviceType}, ProcessorModel={processorModel}, RamCapacity={ramCapacity}, StorageCapacity={storageCapacity}, OperatingSystem={operatingSystem})";
    }
}