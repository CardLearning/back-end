namespace CardLearning.Application.DTO.Output;

public class CardDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
    public int DeckId { get; set; }
}