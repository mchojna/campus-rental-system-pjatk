namespace CampusRentalSystem.Models.Devices;

public class Projector(
    string name,
    string brand,
    double weight,
    DateOnly purchaseDate,
    string displayTechnology,
    string resolution,
    string brightness,
    string contrast)
    : Device(name, brand, weight, purchaseDate)
{
    public static DeviceType _deviceType = DeviceType.Projector;

    public string DisplayTechnology { get; set; } = displayTechnology;
    public string Resolution { get; set; } = resolution;
    public string Brightness { get; set; } = brightness;
    public string Contrast { get; set; } = contrast;
}