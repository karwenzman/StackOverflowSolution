using SampleTextBoxValidation.ViewModels.Interfaces;
using System.Windows;

namespace SampleTextBoxValidation.Views.Windows;

public partial class ShellView : Window
{
    private readonly IShellViewModel _viewModel;

    public ShellView(IShellViewModel viewModel)
    {
        _viewModel = viewModel;

        DataContext = _viewModel;

        InitializeComponent();
    }
}
