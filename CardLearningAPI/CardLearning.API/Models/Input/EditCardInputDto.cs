namespace CardLearningAPI.Models.Input;

public class EditCardInputDto
{
    public required string Name { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
}