namespace CardLearningAPI.Models.Output;

public class DeckOutputDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}