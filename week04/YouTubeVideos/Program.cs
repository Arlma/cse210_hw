
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Learning C# Basics", "John Doe", 600);
        Video video2 = new Video("Object-Oriented Programming Explained", "Jane Smith", 1200);
        Video video3 = new Video("Top 10 Programming Tips", "Tech Guru", 900);

        // Add comments to video1
        video1.AddComment(new Comment("Alice", "Great tutorial, very easy to follow!"));
        video1.AddComment(new Comment("Bob", "Thanks, this helped me a lot."));
        video1.AddComment(new Comment("Charlie", "Can you make one for advanced topics?"));

        // Add comments to video2
        video2.AddComment(new Comment("Diana", "Very clear explanation!"));
        video2.AddComment(new Comment("Ethan", "OOP finally makes sense now."));
        video2.AddComment(new Comment("Frank", "Loved the real-world examples."));

        // Add comments to video3
        video3.AddComment(new Comment("Grace", "Tip #3 was a game changer for me."));
        video3.AddComment(new Comment("Henry", "Short and useful, thanks!"));
        video3.AddComment(new Comment("Isla", "Please do more videos like this."));

        // Put all videos into a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display info for each video
        foreach (Video v in videos)
        {
            v.DisplayVideoInfo();
        }
    }
}