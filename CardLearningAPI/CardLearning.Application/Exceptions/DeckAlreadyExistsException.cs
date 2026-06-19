namespace CardLearning.Application.Exceptions;

public class DeckAlreadyExistsException : ApplicationExceptionBase
{
    public DeckAlreadyExistsException(string message) : base(message)
    {
        
    }
}
