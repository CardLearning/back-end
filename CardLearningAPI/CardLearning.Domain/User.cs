namespace CardLearning.Domain;

public class User
{
    public int Id { get; set; }
    public required string  UserName { get; set; }
    public required string Email { get; set; }
}