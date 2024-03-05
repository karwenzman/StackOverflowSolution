using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.IO;

namespace SampleConverterCulture.ViewModels;

public partial class InfoViewModel : ViewModelBase, IInfoViewModel
{
	[ObservableProperty] private string? _infoText;
	[ObservableProperty] private string? _screenErrorMessage = "No error!";
	[ObservableProperty] private bool _isVisibleErrorMessage = false;
	[ObservableProperty] private string? _screenTitle = "Info Screen";
	[ObservableProperty] private string? _screenFooter;

	public InfoViewModel()
	{
		var currentPath = Path.Combine(Environment.CurrentDirectory, "Documentations", "Info.txt");
		var cultureInfo = CultureInfo.CurrentCulture;
		var cultureUiInfo = CultureInfo.CurrentUICulture;

		if (File.Exists(currentPath))
		{
			InfoText = File.ReadAllText(currentPath);
		}
		else
		{
			InfoText = null;
			ScreenErrorMessage = "The Info.txt file is not found. Check the Documentations folder.";
			IsVisibleErrorMessage = true;
		}

		ScreenFooter = $"Path: {currentPath}";
	}

}
