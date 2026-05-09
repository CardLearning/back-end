using CardLearning.Domain.Enums;

namespace Dom;

public class LearningSession
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public int UserId { get; set; }
    public LearningSessionEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}