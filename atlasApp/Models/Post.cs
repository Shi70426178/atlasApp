namespace atlasApp.Models;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string CreatedByName { get; set; }

    public DateTime CreatedDate { get; set; }
}