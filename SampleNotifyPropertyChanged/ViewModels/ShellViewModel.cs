using Microsoft.Extensions.DependencyInjection;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;

namespace SampleNotifyPropertyChanged.ViewModels;

public class ShellViewModel : IShellViewModel
{
    public object? CurrentViewModel { get; set; } = new();
    public string ApplicationName { get; set; } = "Sample NotifyPropertyChanged Application";

    public ShellViewModel()
    {
        CurrentViewModel = App.AppHost!.Services.GetRequiredService<IPersonViewModel>();
    }
}
