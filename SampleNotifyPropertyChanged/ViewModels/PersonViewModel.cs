using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleNotifyPropertyChanged.Models;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;
using StackOverflow.Library.Models;

namespace SampleNotifyPropertyChanged.ViewModels;

public partial class PersonViewModel : ViewModelBase, IPersonViewModel
{
    [ObservableProperty]
    private PersonDisplayModel _person = new();

    public PersonViewModel()
    {
        Person.PropertyChanged += Person_PropertyChanged;
    }

    /// <summary>
    /// This event handler is finally calling NotifyCanExecuteChanged().
    /// The type PersonDisplayModel is implementing ObservableProperty and OberservableValidation for every property.
    /// Since this class is not aware of the commands in this ViewModel the CanExcecute logic can not called directly
    /// by the model's properties.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Person_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        DiscardButtonCommand.NotifyCanExecuteChanged();
        SaveDataButtonCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void LoadDataButton()
    {
        Person.MapFrom(PersonModel.LoadDataFromRepository());
    }

    [RelayCommand(CanExecute = nameof(CanSaveDataButton))]
    public void SaveDataButton()
    {
        PersonModel.SaveDataToRepository(Person.MapTo(new PersonModel()));
    }
    public bool CanSaveDataButton()
    {
        return !Person.HasErrors;
    }

    [RelayCommand(CanExecute = nameof(CanDiscardButton))]
    public void DiscardButton()
    {
        Person.GetBackupValues();
    }
    public bool CanDiscardButton()
    {
        return Person.HaveValuesChanged();
    }

}
