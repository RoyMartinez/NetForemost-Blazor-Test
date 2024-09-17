
namespace Netforemost_Todo_Api.Share;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class FetchException : Exception
{
    public FetchException(string message) : base(message) { }
}

public class CreateException : Exception
{
    public CreateException(string message) : base(message) { }
}
public class UpdateException : Exception
{
    public UpdateException(string message) : base(message) { }
}

public class RemoveException : Exception
{
    public RemoveException(string message) : base(message) { }
}