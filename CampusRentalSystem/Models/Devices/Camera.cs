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
    private static DeviceType _deviceType = DeviceType.Camera;

    protected double BaseAperture { get; set; } = baseAperture;
    protected double BaseShutterSpeed { get; set; } = baseShutterSpeed;
    protected double BaseIso { get; set; } = baseIso;
    public string SensorType { get; set; } = sensorType;
}