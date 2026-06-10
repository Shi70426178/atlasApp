namespace atlasApp.Models;

public class CreatePostRequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int CreatedBy { get; set; }

    public string CreatedByName { get; set; }

    public DateTime CreatedDate { get; set; }
}