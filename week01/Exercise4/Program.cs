using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        List<double> numbers = GetNumbers();

        double total = CalculateSum(numbers);
        double average = CalculateAverage(numbers);
        double maxNum = FindMax(numbers);

        Console.WriteLine($"The sum is: {total}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNum}");

        double? smallestPositive = FindSmallestPositive(numbers);
        if (smallestPositive.HasValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        double[] sortedNumbers = SortNumbers(numbers);
        Console.WriteLine($"The sorted list is: {string.Join(" ", sortedNumbers)}");
    }

    static List<double> GetNumbers()
    {
        List<double> numbers = new List<double>();
        while (true)
        {
            Console.Write("Enter number: ");
            double num = Convert.ToDouble(Console.ReadLine());
            if (num == 0)
            {
                break;
            }
            numbers.Add(num);
        }
        return numbers;
    }

    static double CalculateSum(List<double> numbers)
    {
        return numbers.Sum();
    }

    static double CalculateAverage(List<double> numbers)
    {
        return numbers.Average();
    }

    static double FindMax(List<double> numbers)
    {
        return numbers.Max();
    }

    static double? FindSmallestPositive(List<double> numbers)
    {
        var positiveNumbers = numbers.Where(n => n > 0);
        return positiveNumbers.Any() ? positiveNumbers.Min() : (double?)null;
    }

    static double[] SortNumbers(List<double> numbers)
    {
        return numbers.OrderBy(n => n).ToArray();
    }
}
