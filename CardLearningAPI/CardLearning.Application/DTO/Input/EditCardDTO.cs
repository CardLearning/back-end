namespace CardLearning.Application.DTO.Input;

public class EditCardDTO
{
    public required string Name { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
}