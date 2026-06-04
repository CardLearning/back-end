namespace CardLearning.Domain;

public class LearningSessionProgress
{
    public int Id { get; set; }
    public int LearningSessionId { get; set; }
    public ICollection<LearningSessionCardResult> CardResults { get; set; } = [];
}