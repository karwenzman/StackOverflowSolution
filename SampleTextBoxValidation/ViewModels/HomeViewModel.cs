using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleTextBoxValidation.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows;

namespace SampleTextBoxValidation.ViewModels;

public partial class HomeViewModel : ViewModelBase, IHomeViewModel
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This field can not be empty!")]
    private string? _firstName = null;
    private string? _backupFirstName = null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This field can not be empty!")]
    private string? _lastName = null;
    private string? _backupLastName = null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Range(9, 999, ErrorMessage = "Value is out of range")]
    private int _intValue = 0;
    private int _backupIntValue = 0;

    [ObservableProperty]
    private string? _screenTitle = "The first column uses the standard WPF controls without any modifaction or style." +
        "\nThe second column uses the 'DefaultTextBoxStyle', where the custom control is based on." +
        "\nThe third column is a custom control.";
    [ObservableProperty]
    private string? _screenFooter = $"The current culture is: {CultureInfo.CurrentCulture.Name}"; 
    [ObservableProperty]
    private int _value = 0; // Temporally for testing.

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Range(9, 999, ErrorMessage = "Value is out of range")]
    private decimal _decimalValue = 0;
    private decimal _backupDecimalValue = 0;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Range(9, 999, ErrorMessage = "Value is out of range")]
    private double _doubleValue = 0;
    private double _backupDoubleValue = 0;

    [RelayCommand(CanExecute = nameof(CanDiscardButton))]
    public void DiscardButton()
    {
        FirstName = _backupFirstName;
        LastName = _backupLastName;
        IntValue = _backupIntValue;
        Value = _backupIntValue; // Temporally for testing.
        DecimalValue = _backupDecimalValue;
        DoubleValue = _backupDoubleValue;
    }
    public bool CanDiscardButton()
    {
        if (FirstName != _backupFirstName)
        {
            return true;
        }
        else if (LastName != _backupLastName)
        {
            return true;
        }
        else if (IntValue != _backupIntValue)
        {
            return true;
        }
        else if (DecimalValue != _backupDecimalValue)
        {
            return true;
        }
        else if (DoubleValue != _backupDoubleValue)
        {
            return true;
        }
        return false;
    }

    [RelayCommand]
    public void LoadDataButton()
    {
        FirstName = "Jane";
        _backupFirstName = FirstName;
        LastName = "Doe";
        _backupLastName = LastName;
        IntValue = 30;
        _backupIntValue = IntValue;
        Value = _backupIntValue; // Temporally for testing.
        DecimalValue = 123.4567m;
        _backupDecimalValue = DecimalValue;
        DoubleValue = 123123.4567d;
        _backupDoubleValue = DoubleValue;

        DiscardButtonCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanSaveDataButton))]
    public void SaveDataButton()
    {
        _backupFirstName = FirstName;
        _backupLastName = LastName;
        _backupIntValue = IntValue;
        _backupDecimalValue = DecimalValue;
        _backupDoubleValue = DoubleValue;

        DiscardButtonCommand.NotifyCanExecuteChanged();
        MessageBox.Show("Values moved to private backup fields.", "SaveButton");
    }
    public bool CanSaveDataButton()
    {
        return !HasErrors;
    }

}
