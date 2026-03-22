namespace CampusRentalSystem.Models.Devices;

public class Camera(
    string name,
    string brand,
    double weight,
    DateOnly purchaseDate,
    double baseAperture,
    double baseShutterSpeed,
    double baseIso,
    string sensorType)
    : Device(name, brand, weight, purchaseDate)
{
    public static readonly DeviceType DeviceType = DeviceType.Camera;

    public override string ToString()
    {
        return
            $"Camera (Id={Id}, Name={Name}, Brand={Brand}, Weight={Weight}, PurchaseDate={PurchaseDate}, DeviceStatus={DeviceStatus}, DeviceType={DeviceType}, BaseAperture={baseAperture}, BaseShutterSpeed={baseShutterSpeed}, BaseIso={baseIso}, SensorType={sensorType})";
    }
}