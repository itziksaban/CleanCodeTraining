namespace CleanCodeMiniTasks.Task1;
public interface IRaceVehicle
{
    double MaxSpeed { get; }

    double AccelerationFactor { get; }

    TimeOnly TimeUntilNextWheelChange();

    double CalculateCurrentSpeed(double friction);
}

public class RaceCar : IRaceVehicle
{
    private WheelCondition[] _wheelsConditions = new WheelCondition[4];

    public double MaxSpeed => 300d;

    public double AccelerationFactor => 1.1d;

    public TimeOnly TimeUntilNextWheelChange()
    {
        // Complex calculation that uses _wheelsConditions
        return TimeOnly.FromDateTime(DateTime.Now).AddMinutes(2);
    }

    public double CalculateCurrentSpeed(double friction)
    {
        return Math.Min(MaxSpeed, CalculateSpeed());

        double CalculateSpeed()
        {
            // Complex calculation that uses all relevant factors
            return 124;
        }
    }
}

public class RaceBoat : IRaceVehicle
{
    public double MaxSpeed => 150d;

    public double AccelerationFactor => 1.05d;

    public TimeOnly TimeUntilNextWheelChange()
    {
        // Since RaceBoat doesn't have any wheels,
        // let's return something that indicates that it never needs to change wheels.
        return TimeOnly.MaxValue;
    }

    public double CalculateCurrentSpeed(double friction)
    {
        return Math.Min(MaxSpeed, CalculateSpeed());

        double CalculateSpeed()
        {
            // Complex calculation that uses all relevant factors
            return 88;
        }
    }
}

public enum WheelCondition
{
    BrandNew,
    SlightlyUsed,
    ModeratelyUsed,
    WellUsed,
    WornOut
}
