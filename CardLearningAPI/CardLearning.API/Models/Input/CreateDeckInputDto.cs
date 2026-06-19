namespace CardLearningAPI.Models.Input;

public class CreateDeckInputDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}