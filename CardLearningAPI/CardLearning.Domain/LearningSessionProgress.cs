namespace CardLearning.Domain;

public class LearningSessionProgress
{
    public int LearningSessionId { get; set; }
    public IEnumerable<int>? SuccessfulCards { get; set; } = null!;
    public IEnumerable<int>? FailedCards { get; set; } = null;
    public IEnumerable<int>? SkippedCards { get; set; } = null;
    public int TotalCards { get; set; }
}