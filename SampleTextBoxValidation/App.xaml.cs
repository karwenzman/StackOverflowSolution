using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleTextBoxValidation.ViewModels;
using SampleTextBoxValidation.ViewModels.Interfaces;
using SampleTextBoxValidation.Views.Windows;
using System.Diagnostics;
using System.Windows;

namespace SampleTextBoxValidation;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        // Validate the environment variable.
        try
        {
            var environmentVariable = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(environmentVariable))
            {
                environmentVariable = "Production";
            }
            if (!(environmentVariable == "Development" || environmentVariable == "Production"))
            {
                throw new Exception("The file 'launchSettings.json' contains an incorrect value for variable DOTNET_ENVIRONMENT.");
            }
        }
        catch (Exception ex)
        {
            ShowMessageBox(ex.Message, "launchSettings.json");
            System.Windows.Application.Current.Shutdown();

            // TODO - How to exit properly?
        }


        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Adds Windows and its ViewModels.
                services.AddSingleton<ShellView>();
                services.AddSingleton<IShellViewModel, ShellViewModel>();

                // Adds the Screen's ViewModels.
                services.AddTransient<IHomeViewModel, HomeViewModel>();
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
            ShowMessageBox(ex.Message, nameof(OnStartup));
            System.Windows.Application.Current.Shutdown();
            // TODO - How to exit properly?
        }

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }

    /// <summary>
    /// This method is calling <see cref="MessageBox.Show()"/>.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="caption"></param>
    /// <returns></returns>
    private static MessageBoxResult ShowMessageBox(string message, string caption)
    {
        MessageBoxResult messageBoxResult = MessageBox.Show(
            messageBoxText: message,
            caption: caption,
            MessageBoxButton.OK,
            MessageBoxImage.Error,
            MessageBoxResult.No);

        return messageBoxResult;
    }
}
