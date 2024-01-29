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
    private string _firstName = string.Empty;
    private string _backupFirstName = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveDataButtonCommand))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This field can not be empty!")]
    private string _lastName = string.Empty;
    private string _backupLastName = string.Empty;

    [RelayCommand(CanExecute = nameof(CanDiscardButton))]
    public void DiscardButton()
    {
        FirstName = _backupFirstName;
        LastName = _backupLastName;
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

        return false;
    }

    [RelayCommand]
    public void LoadDataButton()
    {
        FirstName = "Jane";
        _backupFirstName = FirstName;
        LastName = "Doe";
        _backupLastName = LastName;
        DiscardButtonCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanSaveDataButton))]
    public void SaveDataButton()
    {
        _backupFirstName = FirstName;
        _backupLastName = LastName;
        DiscardButtonCommand.NotifyCanExecuteChanged();
        MessageBox.Show("Values moved to private backup fields.", "SaveButton");
    }
    public bool CanSaveDataButton()
    {
        return !HasErrors;
    }

}
