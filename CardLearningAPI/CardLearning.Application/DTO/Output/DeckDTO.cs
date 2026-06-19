namespace CardLearning.Application.DTO.Output;

public class DeckDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}