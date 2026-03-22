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
    : Device(name, brand, weight, purchaseDate, DeviceType.Projector)
{
    public override string ToString()
    {
        return
            $"Projector (Id={Id}, Name={Name}, Brand={Brand}, Weight={Weight}, PurchaseDate={PurchaseDate}, DeviceStatus={DeviceStatus}, " +
            $"DeviceType={DeviceType}, DisplayTechnology={displayTechnology}, Resolution={resolution}, Brightness={brightness}, Contrast={contrast})";
    }
}