using System;
using System.Threading.Tasks;

namespace WPF.MVVM.Command;

public class AsyncCommand : BaseAsyncCommand<object?, object?>, IAsyncCommand
{
    public AsyncCommand(Func<Task> execute, Func<object?, bool>? canExecute = null, Action<Exception>? onException = null, bool continueOnCapturedContext = false)
        : base(ConvertExecute(execute), canExecute, onException, continueOnCapturedContext)
    { }

    public Task ExecuteAsync() => ExecuteAsync(null);

    static Func<object?, Task>? ConvertExecute(Func<Task> execute)
    {
        if (execute is null)
        {
            return null;
        }

        return _ => execute();
    }
}

public class AsyncCommand<TExecute> : BaseAsyncCommand<TExecute, object?>, IAsyncCommand<TExecute>
{
    public AsyncCommand(Func<TExecute?, Task> execute, Func<object?, bool>? canExecute = null, Action<Exception>? onException = null, bool continueOnCapturedContext = false)
        : base(execute, canExecute, onException, continueOnCapturedContext)
    { }

    public new Task ExecuteAsync(TExecute parameter)
    {
        return base.ExecuteAsync(parameter);
    }
}

public class AsyncCommand<TExecute, TCanExecute> : BaseAsyncCommand<TExecute, TCanExecute>, IAsyncCommand<TExecute, TCanExecute>
{
    public AsyncCommand(Func<TExecute?, Task> execute, Func<TCanExecute?, bool>? canExecute = null, Action<Exception>? onException = null, bool continueOnCapturedContext = false)
        : base(execute, canExecute, onException, continueOnCapturedContext)
    { }

    public new Task ExecuteAsync(TExecute parameter)
    {
        return base.ExecuteAsync(parameter);
    }
}