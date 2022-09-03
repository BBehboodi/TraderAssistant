using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.MVVM.SafeFireAndForget;

namespace WPF.MVVM.Command;

public abstract class BaseAsyncCommand<TExecute, TCanExecute> : BaseCommand<TCanExecute>, ICommand
{
    private readonly Func<TExecute?, Task> _execute;
    private readonly Action<Exception>? _onException;
    private readonly bool _continueOnCapturedContext;

    protected private BaseAsyncCommand(Func<TExecute?, Task>? execute, Func<TCanExecute?, bool>? canExecute, Action<Exception>? onException, bool continueOnCapturedContext)
        : base(canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _onException = onException;
        _continueOnCapturedContext = continueOnCapturedContext;
    }

    private protected Task ExecuteAsync(TExecute? parameter)
    {
        return _execute(parameter);
    }

    bool ICommand.CanExecute(object? parameter)
    {
        switch (parameter)
        {
            case TCanExecute validParameter:
                {
                    return CanExecute(validParameter);
                }
            case null when IsNullable<TCanExecute>():
                {
                    return CanExecute((TCanExecute?)parameter);
                }
            case null:
                {
                    throw new InvalidCommandParameterException(typeof(TCanExecute));
                }
            default:
                {
                    throw new InvalidCommandParameterException(typeof(TCanExecute), parameter.GetType());
                }
        }
    }

    void ICommand.Execute(object? parameter)
    {
        switch (parameter)
        {
            case TExecute validParameter:
                {
                    ExecuteAsync(validParameter).SafeFireAndForget(_onException, _continueOnCapturedContext);
                }
                break;
            case null when IsNullable<TExecute>():
                {
                    ExecuteAsync((TExecute?)parameter).SafeFireAndForget(_onException, _continueOnCapturedContext);
                }
                break;
            case null:
                {
                    throw new InvalidCommandParameterException(typeof(TExecute));
                }
            default:
                {
                    throw new InvalidCommandParameterException(typeof(TExecute), parameter.GetType());
                }
        }
    }
}