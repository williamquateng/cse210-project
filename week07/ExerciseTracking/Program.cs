using System;
using System.Collections.Generic;

// Base Activity class
public abstract class Activity
{
    protected DateTime date;
    protected int minutes;

    public Activity(DateTime date, int minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{date.ToString("dd MMM yyyy")} {GetType().Name} ({minutes} min)- Distance {GetDistance():F1} km, Speed {GetSpeed():F1} kph, Pace: {GetPace():F2} min per km";
    }
}

// Derived Running class
public class Running : Activity
{
    private double distance;

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / minutes) * 60;
    }

    public override double GetPace()
    {
        return minutes / distance;
    }
}

// Derived Cycling class
public class Cycling : Activity
{
    private double speed;

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed / 60) * minutes;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60 / speed;
    }
}

// Derived Swimming class
public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / minutes) * 60;
    }

    public override double GetPace()
    {
        return minutes / GetDistance();
    }
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 30, 9.7),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

//We define an abstract base class Activity with protected fields for date and minutes, and abstract methods for getting distance, speed, and pace.
//We create derived classes Running, Cycling, and Swimming that inherit from Activity and implement the abstract methods.
//Each derived class has its own specific fields and calculations for distance, speed, and pace.
//The GetSummary method is defined in the base class and uses the abstract methods to generate a summary string.
//In the Main method, we create a list of Activity objects and iterate through it, calling GetSummary on each object to display the results.


//This program demonstrates inheritance and polymorphism by creating a base Activity class and derived classes for each activity type.
//The GetSummary method is defined in the base class and uses abstract methods to generate a summary string, showcasing polymorphism.