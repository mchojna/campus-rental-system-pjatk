namespace CampusRentalSystem.Services;

public class PenaltyCalculator
{
    private const double DailyPenaltyRate = 5.0;

    public static double CalculatePenalty(DateOnly expectedReturnDate, DateOnly actualReturnDate)
    {
        if (actualReturnDate <= expectedReturnDate) return 0.0;

        var daysLate = actualReturnDate.DayNumber - expectedReturnDate.DayNumber;
        return daysLate * DailyPenaltyRate;
    }
}