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
}

// Nextclass:

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

// Finallyclass:

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
        for (int i = 0; i < count; i++)
        {
            var word = Words[random.Next(Words.Count)];
            if (!word.IsHidden)
            {
                word.IsHidden = true;
            }
        }
    }
}
