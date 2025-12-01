using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

// Base class for all goal types
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();
    public abstract string GetStatus();
}

// Simple goal: marked complete and earns points
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        IsComplete = true;
    }

    public override string GetStatus()
    {
        return IsComplete ? "[X]" : "[ ]";
    }
}

// Eternal goal: earns points each time it's recorded
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        // No completion status change
    }

    public override string GetStatus()
    {
        return "[ ]";
    }
}

// Checklist goal: earns points each time and a bonus when complete
public class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        CurrentCount++;
        if (CurrentCount >= TargetCount)
        {
            IsComplete = true;
        }
    }

    public override string GetStatus()
    {
        return $"Completed {CurrentCount}/{TargetCount} times";
    }
}

public class GoalConverter : JsonConverter<Goal>
{
    public override Goal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        string type = null;
        string name = null;
        int points = 0;
        int targetCount = 0;
        int currentCount = 0;
        int bonusPoints = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "Type":
                        type = reader.GetString();
                        break;
                    case "Name":
                        name = reader.GetString();
                        break;
                    case "Points":
                        points = reader.GetInt32();
                        break;
                    case "TargetCount":
                        targetCount = reader.GetInt32();
                        break;
                    case "CurrentCount":
                        currentCount = reader.GetInt32();
                        break;
                    case "BonusPoints":
                        bonusPoints = reader.GetInt32();
                        break;
                }
            }
        }

        Goal goal = null;
        if (type == "SimpleGoal")
        {
            goal = new SimpleGoal(name, points);
        }
        else if (type == "EternalGoal")
        {
            goal = new EternalGoal(name, points);
        }
        else if (type == "ChecklistGoal")
        {
            goal = new ChecklistGoal(name, points, targetCount, bonusPoints);
            ((ChecklistGoal)goal).CurrentCount = currentCount;
        }

        return goal;
    }

    public override void Write(Utf8JsonWriter writer, Goal value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Type", value.GetType().Name);
        writer.WriteString("Name", value.Name);
        writer.WriteNumber("Points", value.Points);

        if (value is ChecklistGoal checklistGoal)
        {
            writer.WriteNumber("TargetCount", checklistGoal.TargetCount);
            writer.WriteNumber("CurrentCount", checklistGoal.CurrentCount);
            writer.WriteNumber("BonusPoints", checklistGoal.BonusPoints);
        }

        writer.WriteEndObject();
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main(string[] args)
    {
        LoadGoals();

        while (true)
        {
            Console.WriteLine("Eternal Quest");
            Console.WriteLine("1. Create new goal");
            Console.WriteLine("2. Record event");
            Console.WriteLine("3. Show goals");
            Console.WriteLine("4. Show score");
            Console.WriteLine("5. Save and exit");

            Console.Write("Choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    RecordEvent();
                    break;
                case 3:
                    ShowGoals();
                    break;
                case 4:
                    ShowScore();
                    break;
                case 5:
                    SaveGoals();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Create new goal");
        Console.WriteLine("1. Simple goal");
        Console.WriteLine("2. Eternal goal");
        Console.WriteLine("3. Checklist goal");

        Console.Write("Choose a goal type: ");
        int goalType = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter points: ");
        int points = Convert.ToInt32(Console.ReadLine());

        switch (goalType)
        {
            case 1:
                goals.Add(new SimpleGoal(name, points));
                break;
            case 2:
                goals.Add(new EternalGoal(name, points));
                break;
            case 3:
                Console.Write("Enter target count: ");
                int targetCount = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter bonus points: ");
                int bonusPoints = Convert.ToInt32(Console.ReadLine());

                goals.Add(new ChecklistGoal(name, points, targetCount, bonusPoints));
                break;
            default:
                Console.WriteLine("Invalid goal type. Please try again.");
                break;
        }
    }

    static void RecordEvent()
    {
        Console.WriteLine("Record event");
        Console.WriteLine("Select a goal:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }

        Console.Write("Enter goal number: ");
        int goalIndex = Convert.ToInt32(Console.ReadLine()) - 1;

        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            score += goals[goalIndex].Points;

            if (goals[goalIndex] is ChecklistGoal checklistGoal && checklistGoal.IsComplete)
            {
                score += checklistGoal.BonusPoints;
            }
        }
        else
        {
            Console.WriteLine("Invalid goal number. Please try again.");
        }
    }

    static void ShowGoals()
    {
        Console.WriteLine("Goals");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name} - {goals[i].GetStatus()}");
        }
    }

    static void ShowScore()
    {
        Console.WriteLine($"Score: {score}");
    }

    static void SaveGoals()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        options.Converters.Add(new GoalConverter());
        string json = JsonSerializer.Serialize(goals, options);
        string scoreJson = JsonSerializer.Serialize(score);

        File.WriteAllText("goals.json", json);
        File.WriteAllText("score.json", scoreJson);
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.json") && File.Exists("score.json"))
        {
            string json = File.ReadAllText("goals.json");
            string scoreJson = File.ReadAllText("score.json");

            var options = new JsonSerializerOptions();
            options.Converters.Add(new GoalConverter());
            goals = JsonSerializer.Deserialize<List<Goal>>(json, options);
            score = JsonSerializer.Deserialize<int>(scoreJson);
        }
    }
}
