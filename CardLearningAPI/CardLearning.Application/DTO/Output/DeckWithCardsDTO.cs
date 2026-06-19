namespace CardLearning.Application.DTO.Output;

public class DeckWithCardsDTO
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<CardDTO>? Cards { get; set; }
}