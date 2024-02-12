using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleTextBoxValidation.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;
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
    [Required(ErrorMessage = "This field can not be empty!")]
    private string? _intValueAsString = null;
    private string? _backupIntValueAsString = null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This field can not be empty!")]
    private string? _decimalValueAsString = null;
    private string? _backupDecimalValueAsString = null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This field can not be empty!")]
    private string? _doubleValueAsString = null;
    private string? _backupDoubleValueAsString = null;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Range(9, 999, ErrorMessage = "Value is out of range")]
    private int _intValue = 0;
    private int _backupIntValue = 0;

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
        DecimalValue = _backupDecimalValue;
        DoubleValue = _backupDoubleValue;
        IntValueAsString = _backupIntValueAsString;
        DecimalValueAsString = _backupDecimalValueAsString;
        DoubleValueAsString = _backupDoubleValueAsString;
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
        else if (IntValueAsString != _backupIntValueAsString)
        {
            return true;
        }
        else if (DecimalValueAsString != _backupDecimalValueAsString)
        {
            return true;
        }
        else if (DoubleValueAsString != _backupDoubleValueAsString)
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
        IntValueAsString = IntValue.ToString();
        _backupIntValueAsString = IntValueAsString;
        DecimalValue = 123.4567m;
        _backupDecimalValue = DecimalValue;
        DecimalValueAsString = DecimalValue.ToString();
        _backupDecimalValueAsString = DecimalValueAsString;
        DoubleValue = 123123.4567d;
        _backupDoubleValue = DoubleValue;
        DoubleValueAsString = DoubleValue.ToString();
        _backupDoubleValueAsString = DoubleValueAsString;

        DiscardButtonCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanSaveDataButton))]
    public void SaveDataButton()
    {
        _backupFirstName = FirstName;
        _backupLastName = LastName;
        _backupIntValueAsString = IntValueAsString;
        _backupDecimalValueAsString = DecimalValueAsString;
        _backupDoubleValueAsString = DoubleValueAsString;
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
