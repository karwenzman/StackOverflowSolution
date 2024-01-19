using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;

namespace SampleNotifyPropertyChanged.ViewModels;

public partial class ShellViewModel : ViewModelBase, IShellViewModel
{
    [ObservableProperty]
    private object? _currentViewModel = new();
    
    [ObservableProperty]
    private string _applicationName = "Project NotifyPropertyChanged";

    public ShellViewModel()
    {
        CurrentViewModel = App.AppHost!.Services.GetRequiredService<IPersonViewModel>();
    }
}
