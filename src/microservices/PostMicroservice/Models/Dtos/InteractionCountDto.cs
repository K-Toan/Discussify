namespace PostMicroservice.Models.Dtos;

public class InteractionCountDto
{
    public int Upvote { get; set; } = 0;
    public int Downvote { get; set; } = 0;
    public int Comment { get; set; } = 0;
}