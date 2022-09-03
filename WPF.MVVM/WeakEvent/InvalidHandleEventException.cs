using System;
using System.Reflection;

namespace WPF.MVVM.WeakEvent;

internal class InvalidHandleEventException : Exception
{
    public InvalidHandleEventException(string message, TargetParameterCountException targetParameterCountException)
        : base(message, targetParameterCountException)
    { }
}