using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace TraderAssistant.UI;

public partial class App : Application
{
    public IHost AppHost { get; }

    public App()
    {
        AppHost = Host
            .CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        MainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        base.OnExit(e);
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
    }
}
