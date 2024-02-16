using System;

namespace BO
{
    /// <summary>
    /// Exception thrown when a bl operation fails because the target does not exist.
    /// </summary>
    [Serializable]
    public class BlDoesNotExistException : Exception
    {
        public BlDoesNotExistException(string? message) : base(message) { }
        public BlDoesNotExistException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when a bl operation fails because the target already exists.
    /// </summary>
    [Serializable]
    public class BlAlreadyExistsException : Exception
    {
        public BlAlreadyExistsException(string? message) : base(message) { }
        public BlAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when a bl operation fails because a null property.
    /// </summary>
    [Serializable]
    public class BlNullPropertyException : Exception
    {
        public BlNullPropertyException(string? message) : base(message) { }
    }

    /// <summary>
    /// Exception thrown when deletion operation in bl is not possible.
    /// </summary>
    [Serializable]
    public class BlDeletionImpossible : Exception
    {
        public BlDeletionImpossible(string? message) : base(message) { }
    }

    /// <summary>
    /// Exception thrown when a bl operation fails to read data.
    /// </summary>
    [Serializable]
    public class BlReadFailed : Exception
    {
        public BlReadFailed(string? message) : base(message) { }
        public BlReadFailed(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when input provided to a bl operation is invalid.
    /// </summary>
    [Serializable]
    public class BlInvalidInputException : Exception
    {
        public BlInvalidInputException(string? message) : base(message) { }
    }

    /// <summary>
    /// Exception thrown when a bl operation fails to create something.
    /// </summary>
    [Serializable]
    public class BlCreatFailed : Exception
    {
        public BlCreatFailed(string? message) : base(message) { }
        public BlCreatFailed(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when a bl operation encounters an illegal situation.
    /// </summary>
    [Serializable]
    public class BlIllegalException : Exception
    {
        public BlIllegalException(string? message) : base(message) { }
        public BlIllegalException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}