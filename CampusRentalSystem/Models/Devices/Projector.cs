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
    private static DeviceType _deviceType = DeviceType.Projector;

    protected string DisplayTechnology { get; set; } = displayTechnology;
    protected string Resolution { get; set; } = resolution;
    protected string Brightness { get; set; } = brightness;
    protected string Contrast { get; set; } = contrast;
}