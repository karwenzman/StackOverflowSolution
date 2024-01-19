using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleNotifyPropertyChanged.ViewModels;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;
using SampleNotifyPropertyChanged.Views.Windows;
using System.Windows;

namespace SampleNotifyPropertyChanged;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Adds Windows and its ViewModels.
                services.AddSingleton<ShellView>();
                services.AddSingleton<IShellViewModel, ShellViewModel>();

                // Adds the Screen's ViewModels.
                services.AddTransient<IPersonViewModel, PersonViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        try
        {
            var shellWindow = AppHost.Services.GetRequiredService<ShellView>();
            shellWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }
}
