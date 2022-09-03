using System;
using System.Reflection;
using WPF.MVVM.WeakEvent;

namespace WPF.MVVM.Command;

public abstract class BaseCommand<TCanExecute>
{
    private readonly Func<TCanExecute?, bool> _canExecute;
    private readonly WeakEventManager _weakEventManager = new();

    protected private BaseCommand(Func<TCanExecute?, bool>? canExecute) => _canExecute = canExecute ?? (_ => true);

    public event EventHandler? CanExecuteChanged
    {
        add => _weakEventManager.AddEventHandler(value!);
        remove => _weakEventManager.RemoveEventHandler(value!);
    }

    public bool CanExecute(TCanExecute? parameter)
    {
        return _canExecute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        _weakEventManager.RaiseEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));
    }

    private protected static bool IsNullable<T>()
    {
        var type = typeof(T);

        if (!type.GetTypeInfo().IsValueType)
        {
            return true;
        }

        if (Nullable.GetUnderlyingType(type) != null)
        {
            return true;
        }

        return false;
    }
}