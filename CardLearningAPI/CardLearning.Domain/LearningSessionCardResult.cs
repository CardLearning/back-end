using CardLearning.Domain.Enums;

namespace CardLearning.Domain;

public class LearningSessionCardResult
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public Card Card { get; set; } = null!;
    public int LearningSessionProgressId { get; set; }
    public CardResultSessionStatus Status { get; set; }
}