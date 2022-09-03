using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.MVVM.Command;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync();

    void RaiseCanExecuteChanged();
}

public interface IAsyncCommand<in T> : ICommand
{
    Task ExecuteAsync(T parameter);

    void RaiseCanExecuteChanged();
}

public interface IAsyncCommand<in TExecute, in TCanExecute> : IAsyncCommand<TExecute>
{
    bool CanExecute(TCanExecute parameter);
}