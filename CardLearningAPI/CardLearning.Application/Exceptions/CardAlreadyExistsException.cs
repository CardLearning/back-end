namespace CardLearning.Application.Exceptions;

public class CardAlreadyExistsException : ApplicationExceptionBase
{
    public CardAlreadyExistsException(string message) : base(message)
    {
        
    }
}
