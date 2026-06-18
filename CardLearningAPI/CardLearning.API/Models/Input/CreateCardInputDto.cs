namespace CardLearningAPI.Models.Input;

public class CreateCardInputDto
{
    public required string Name { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
    public int DeckId { get; set; }
}