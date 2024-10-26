namespace PostMicroservice.Models.Dtos;

public class UpdatePostDto
{
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}