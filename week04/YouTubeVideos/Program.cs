using System;
using System.Collections.Generic;

public class Comment
{
    public string Author { get; set; }
    public string Text { get; set; }

    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Video 1", "Author 1", 360);
        video1.AddComment(new Comment("John", "Great video!"));
        video1.AddComment(new Comment("Jane", "Love it!"));
        video1.AddComment(new Comment("Bob", "Awesome content!"));

        Video video2 = new Video("Video 2", "Author 2", 420);
        video2.AddComment(new Comment("Alice", "Fantastic!"));
        video2.AddComment(new Comment("Mike", "Good job!"));
        video2.AddComment(new Comment("Emma", "Well done!"));

        Video video3 = new Video("Video 3", "Author 3", 480);
        video3.AddComment(new Comment("David", "Excellent!"));
        video3.AddComment(new Comment("Sophia", "Superb!"));
        video3.AddComment(new Comment("Oliver", "Great work!"));

        // Add videos to list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display videos and comments
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Comments: {video.GetCommentCount()}");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"  {comment.Author}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}
