using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

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
}
