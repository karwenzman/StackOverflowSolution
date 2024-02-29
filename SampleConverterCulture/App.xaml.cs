using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace SampleConverterCulture;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        SetApplicationCulture();
        ValidateEnvironmentVariable(["Development", "Production"]);

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

    /// <summary>
    /// This method is setting the app's culture information.
    /// <para></para>
    /// The culture information is needed to display date or number values 
    /// according to the cultures format style.
    /// <para></para>
    /// <br></br>- 
    /// https://learn.microsoft.com/en-us/dotnet/api/system.windows.data.binding.converterculture?view=windowsdesktop-8.0
    /// <br></br>- 
    /// If you do not set this property, the binding engine uses the Language property of the binding target object.
    /// In XAML this defaults to "en-US" or inherits the value from the root element (or any element) of the page, 
    /// if one has been explicitly set.
    /// </summary>
    /// <param name="cultureInfo">The standard culture is read from <see cref="CultureInfo.CurrentCulture"/>.</param>
    private static void SetApplicationCulture(CultureInfo? cultureInfo = null)
    {
        // Validate parameters.
        if (cultureInfo == null)
        {
            cultureInfo = CultureInfo.CurrentCulture;
        };

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        // This work around is needed to activate the culture in WPF controls like TextBox.
        FrameworkElement.LanguageProperty.OverrideMetadata(
            forType: typeof(FrameworkElement),
            typeMetadata: new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(cultureInfo.Name)));

        // TODO - Create a custom binding class (e.g., CultureAwareBinding) that automatically sets the ConverterCulture to the current culture when created.
    }

    /// <summary>
    /// This method is validating the environment settings and the existance of the appsettings file.
    /// <para></para>
    /// The key <b>DOTNET_ENVIRONMENT</b> is read from file 'launchSettings.json'.
    /// <br></br>
    /// If the key's value does not correspond with the array of environment settings provided as parameter,
    /// an exception is thrown and the application shuts down.
    /// <br></br>
    /// If no key is found or no array of environment settings is provided, 
    /// the fallback value is 'Production'.
    /// </summary>
    /// <param name="environmentSettings">An array of string representing the values <b>DOTNET_ENVIRONMENT</b> could be set to.</param>
    private static void ValidateEnvironmentVariable(string[]? environmentSettings = null)
    {
        // TODO - Why is launchSettings.json not be updated straight forward?
        try
        {
            var environmentVariable = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            // Validate Parameters.
            if (environmentSettings == null)
            {
                environmentSettings = ["Production"];
            }

            if (string.IsNullOrWhiteSpace(environmentVariable))
            {
                environmentVariable = "Production";
            }

            // Validate environment variable.
            bool isInvalid = true;
            foreach (var setting in environmentSettings)
            {
                if (setting == environmentVariable)
                {
                    isInvalid = false;
                }
            }
            if (isInvalid)
            {
                throw new Exception("The file 'launchSettings.json' contains an incorrect value for variable DOTNET_ENVIRONMENT.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            // TODO - How to exit properly? Logging?
            System.Windows.Application.Current.Shutdown();
        }
    }
}
