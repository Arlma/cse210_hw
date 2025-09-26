public class Comment
{
    // Properties (data that belongs to a comment)
    public string CommenterName { get; set; }
    public string Text { get; set; }

    // Constructor (used to build a Comment object)
    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}
