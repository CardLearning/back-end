namespace CardLearning.Domain;

public class Deck
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int CardCount { get; set; }
    public IEnumerable<Card>? Cards { get; set; } = null;
    public int UserId { get; set; }
}