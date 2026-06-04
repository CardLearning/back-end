using CardLearning.Domain.Enums;

namespace CardLearning.Domain;
public class LearningSession
{
    public int Id { get; set; }
    public int DeckId { get; set; }
    public Deck? Deck { get; set; } = null;
    public LearningSessionEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}