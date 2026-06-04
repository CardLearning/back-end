namespace CardLearning.Domain;

public class Deck
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Card> Cards { get; set; } = [];
}