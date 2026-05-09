
namespace CardLearning.Domain;

public class Card
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public int UserId { get; set; }
    public bool IsPublic { get; set; }
}