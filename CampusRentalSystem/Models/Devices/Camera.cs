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
    public static DeviceType _deviceType = DeviceType.Camera;

    public double BaseAperture { get; set; } = baseAperture;
    public double BaseShutterSpeed { get; set; } = baseShutterSpeed;
    public double BaseIso { get; set; } = baseIso;
    public string SensorType { get; set; } = sensorType;
}