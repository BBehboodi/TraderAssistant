namespace WPF.MVVM.DependencyInjection;

public interface IAbstractFactory<T>
    where T : class
{
    T Create();
}