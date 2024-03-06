using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Windows;

namespace SampleConverterCulture;

public partial class App : Application
{
	public static IHost? AppHost { get; private set; }

	public App()
	{
		CultureInfoHelper.Set();
		SetEnvironmentVariable(["Development", "Production"]);

		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((context, services) =>
			{
				// Adds Windows and its ViewModels.
				services.AddSingleton<ShellView>();
				services.AddSingleton<IShellViewModel, ShellViewModel>();

				// Adds the Screen's ViewModels.
				services.AddTransient<IHomeViewModel, HomeViewModel>();
				services.AddTransient<IInfoViewModel, InfoViewModel>();
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
	private static void SetEnvironmentVariable(string[]? environmentSettings = null)
	{
		try
		{
			// Validate Parameters.
			if (environmentSettings == null)
			{
				environmentSettings = ["Production"];
			}

			// Validate environment variable.
			string? environmentVariable = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
			bool isInvalid = true;

			if (string.IsNullOrWhiteSpace(environmentVariable))
			{
				environmentVariable = "Production";
			}

			foreach (var setting in environmentSettings)
			{
				if (setting == environmentVariable)
				{
					isInvalid = false;
				}
			}
			if (isInvalid)
			{
				throw new Exception("\nNo matching value found for key DOTNET_ENVIRONMENT! \nPlease contact developer.");
			}
		}
		catch (Exception ex)
		{
			// TODO - How to exit properly? Logging?
			Debug.WriteLine(ex);
			System.Windows.Application.Current.Shutdown(900);
		}
	}
}
