using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SampleTextBoxValidation.ViewModels.Interfaces;

namespace SampleTextBoxValidation.ViewModels;

public partial class ShellViewModel : ViewModelBase, IShellViewModel
{
    [ObservableProperty]
    private object? _currentViewModel = new();

    [ObservableProperty]
    private string? _applicationName = "SampleTextBoxValidation";

    public ShellViewModel()
    {
        CurrentViewModel = App.AppHost!.Services.GetRequiredService<IHomeViewModel>();
    }
}
