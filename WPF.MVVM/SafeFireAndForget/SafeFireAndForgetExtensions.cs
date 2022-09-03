using System;
using System.Threading.Tasks;

namespace WPF.MVVM.SafeFireAndForget;

public static partial class SafeFireAndForgetExtensions
{
    private static Action<Exception>? _onException;
    private static bool _shouldAlwaysRethrowException;

    public static void SafeFireAndForget(this ValueTask task, Action<Exception>? onException, bool continueOnCapturedContext = false)
    {
        task.SafeFireAndForget(in onException, in continueOnCapturedContext);
    }

    public static void SafeFireAndForget(this ValueTask task, in Action<Exception>? onException = null, in bool continueOnCapturedContext = false)
    {
        HandleSafeFireAndForget(task, continueOnCapturedContext, onException);
    }

    public static void SafeFireAndForget<TException>(this ValueTask task, Action<TException>? onException, bool continueOnCapturedContext = false)
        where TException : Exception
    {
        task.SafeFireAndForget(in onException, in continueOnCapturedContext);
    }

    public static void SafeFireAndForget<TException>(this ValueTask task, in Action<TException>? onException = null, in bool continueOnCapturedContext = false)
        where TException : Exception
    {
        HandleSafeFireAndForget(task, continueOnCapturedContext, onException);
    }

    public static void SafeFireAndForget(this Task task, Action<Exception>? onException, bool continueOnCapturedContext = false)
    {
        task.SafeFireAndForget(in onException, in continueOnCapturedContext);
    }

    public static void SafeFireAndForget(this Task task, in Action<Exception>? onException = null, in bool continueOnCapturedContext = false)
    {
        HandleSafeFireAndForget(task, continueOnCapturedContext, onException);
    }

    public static void SafeFireAndForget<TException>(this Task task, Action<TException>? onException, bool continueOnCapturedContext = false)
        where TException : Exception
    {
        task.SafeFireAndForget(in onException, in continueOnCapturedContext);
    }

    public static void SafeFireAndForget<TException>(this Task task, in Action<TException>? onException = null, in bool continueOnCapturedContext = false)
        where TException : Exception
    {
        HandleSafeFireAndForget(task, continueOnCapturedContext, onException);
    }

    public static void Initialize(bool shouldAlwaysRethrowException)
    {
        Initialize(in shouldAlwaysRethrowException);
    }

    public static void Initialize(in bool shouldAlwaysRethrowException = false)
    {
        _shouldAlwaysRethrowException = shouldAlwaysRethrowException;
    }

    public static void RemoveDefaultExceptionHandling()
    {
        _onException = null;
    }

    public static void SetDefaultExceptionHandling(Action<Exception> onException)
    {
        SetDefaultExceptionHandling(in onException);
    }

    public static void SetDefaultExceptionHandling(in Action<Exception> onException)
    {
        if (onException is null)
        {
            throw new ArgumentNullException(nameof(onException));
        }

        _onException = onException;
    }

    private static async void HandleSafeFireAndForget<TException>(ValueTask valueTask, bool continueOnCapturedContext, Action<TException>? onException)
        where TException : Exception
    {
        try
        {
            await valueTask.ConfigureAwait(continueOnCapturedContext);
        }
        catch (TException ex) when (_onException is not null || onException is not null)
        {
            HandleException(ex, onException);

            if (_shouldAlwaysRethrowException)
            {
                throw;
            }
        }
    }

    private static async void HandleSafeFireAndForget<TException>(Task task, bool continueOnCapturedContext, Action<TException>? onException)
        where TException : Exception
    {
        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
        }
        catch (TException ex) when (_onException is not null || onException is not null)
        {
            HandleException(ex, onException);

            if (_shouldAlwaysRethrowException)
                throw;
        }
    }

    private static void HandleException<TException>(in TException exception, in Action<TException>? onException)
        where TException : Exception
    {
        _onException?.Invoke(exception);
        onException?.Invoke(exception);
    }
}
