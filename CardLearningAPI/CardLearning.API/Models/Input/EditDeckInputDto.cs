namespace CardLearningAPI.Models.Input;

public class EditDeckInputDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}