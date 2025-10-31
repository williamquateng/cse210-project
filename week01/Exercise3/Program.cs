using System;

class Program
{
    static void Main()
    {
        // Generate a random magic num
        Random random = new Random();
        int magicNumber = random.Next(1, 101);

        // Initialize the guess variable
        int guess;

        // Loop until the user guesses the magic number
        do
        {
            // Ask the user for a guess
            Console.Write("What is your guess? ");
            guess = Convert.ToInt32(Console.ReadLine());

            // Determine if the user needs to guess higher or lower
            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
        } while (guess != magicNumber);

        // Tell the user they guessed it
        Console.WriteLine("You guessed it!");
    }
}