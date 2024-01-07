
namespace BO;
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
}

[Serializable]
public class BlReadFailed : Exception
{
    public BlReadFailed(string? message) : base(message) { }
    public BlReadFailed(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlInvalidInputException : Exception
{
    public BlInvalidInputException(string? message) : base(message) { }
}

[Serializable]
public class BlCreatFailed : Exception
{
    public BlCreatFailed(string? message) : base(message) { }
    public BlCreatFailed(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlIllegalException : Exception
{
    public BlIllegalException(string? message) : base(message) { }
    public BlIllegalException(string message, Exception innerException)
                : base(message, innerException) { }
}
