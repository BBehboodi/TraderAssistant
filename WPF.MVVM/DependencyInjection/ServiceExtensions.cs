using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace WPF.MVVM.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddUserControl<TUserControl>(this IServiceCollection services)
        where TUserControl : UserControl
    {
        return services
            .AddTransient<TUserControl>()
            .AddSingleton<Func<TUserControl>>(x => () => x.GetRequiredService<TUserControl>())
            .AddSingleton<IAbstractFactory<TUserControl>, AbstractFactory<TUserControl>>();
    }
}
