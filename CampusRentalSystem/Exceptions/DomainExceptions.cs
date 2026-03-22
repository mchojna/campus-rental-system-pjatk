namespace CampusRentalSystem.Exceptions;

public class DomainException(string message) : Exception(message);

public class NotFoundException(string message) : DomainException(message);

public class AlreadyExistsException(string message) : DomainException(message);

public class RentalLimitExceededException(string message) : DomainException(message);

public class DeviceUnavailableException(string message) : DomainException(message);

public class InvalidDeviceStateException(string message) : DomainException(message);