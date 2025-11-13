using System;
using System.Collections.Generic;
using System.Linq;

public class ScriptureReference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int Verse { get; set; }
    public int? EndVerse { get; set; }

    public ScriptureReference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        Verse = verse;
    }

    public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        Verse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        if (EndVerse.HasValue)
        {
            return $"{Book} {Chapter}:{Verse}-{EndVerse}";
        }
        else
        {
            return $"{Book} {Chapter}:{Verse}";
        }
    }
}

public class ScriptureWord
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    public ScriptureWord(string text)
    {
        Text = text;
        IsHidden = false;
    }
}

public class Scripture
{
    public ScriptureReference Reference { get; set; }
    public List<ScriptureWord> Words { get; set; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(Reference.ToString());
        foreach (var word in Words)
        {
            if (word.IsHidden)
            {
                Console.Write(new string('_', word.Text.Length) + " ");
            }
            else
            {
                Console.Write(word.Text + " ");
            }
        }
        Console.WriteLine();
    }

    public void HideRandomWords(int count)
    {
        var random = new Random();
        var availableWords = Words.Where(w => !w.IsHidden).ToList();
        count = Math.Min(count, availableWords.Count);
        for (int i = 0; i < count; i++)
        {
            var word = availableWords[random.Next(availableWords.Count)];
            word.IsHidden = true;
            availableWords.Remove(word);
        }
    }

    public bool IsFullyHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var reference = new ScriptureReference("John", 3, 16);
        var scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
        while (true)
        {
            scripture.Display();
            Console.Write("Press enter to continue or type 'quit' to exit: ");
            var input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }
            scripture.HideRandomWords(2);
            if (scripture.IsFullyHidden())
            {
                scripture.Display();
                Console.WriteLine("Congratulations, you've hidden the entire scripture!");
                break;
            }
        }
    }
}
