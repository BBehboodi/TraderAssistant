using System;

namespace WPF.MVVM.Command;

public class InvalidCommandParameterException : Exception
{
    public InvalidCommandParameterException(Type expectedType, Type actualType, Exception innerException)
        : base(CreateErrorMessage(expectedType, actualType), innerException)
    { }

    public InvalidCommandParameterException(Type expectedType, Type actualType)
        : base(CreateErrorMessage(expectedType, actualType))
    { }

    public InvalidCommandParameterException(Type expectedType, Exception innerException)
        : base(CreateErrorMessage(expectedType), innerException)
    { }

    public InvalidCommandParameterException(Type expectedType)
        : base(CreateErrorMessage(expectedType))
    { }

    private static string CreateErrorMessage(Type expectedType)
    {
        return $"Invalid type for parameter. Expected Type {expectedType}";
    }

    private static string CreateErrorMessage(Type expectedType, Type actualType)
    {
        return $"Invalid type for parameter. Expected Type {expectedType}, but received Type {actualType}";
    }
}