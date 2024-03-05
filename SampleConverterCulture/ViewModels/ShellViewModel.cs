using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace SampleConverterCulture.ViewModels;

public partial class ShellViewModel : ViewModelBase, IShellViewModel
{
	[ObservableProperty]
	private object? _currentViewModel = new();

	[ObservableProperty]
	private string? _applicationName = "SampleConverterCulture";

	public ShellViewModel()
	{
		CurrentViewModel = App.AppHost!.Services.GetRequiredService<IHomeViewModel>();
	}

	[RelayCommand]
	public void HomeScreenButton()
	{
		CurrentViewModel = App.AppHost!.Services.GetRequiredService<IHomeViewModel>();
	}

	[RelayCommand]
	public void InfoScreenButton()
	{
		CurrentViewModel = App.AppHost!.Services.GetRequiredService<IInfoViewModel>();
	}
}
