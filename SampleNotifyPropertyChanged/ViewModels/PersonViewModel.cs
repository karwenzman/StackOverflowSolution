using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleNotifyPropertyChanged.Models;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Windows.Input;

namespace SampleNotifyPropertyChanged.ViewModels;

public partial class PersonViewModel : ViewModelBase, IPersonViewModel
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    private PersonDisplayModel _person = new();

    //public PersonDisplayModel Person { get; set; } = new();

    public PersonViewModel()
    {
    }
    
    /// <summary>
    /// This code is doing what I am looking for.
    /// </summary>
    private void RequeryOfCanExecuteSuggested()
    {
        DiscardButtonCommand.NotifyCanExecuteChanged();
     
        //CommandManager.InvalidateRequerySuggested(); // not working
    }

    [RelayCommand(CanExecute = nameof(CanLoadDataButton))]
    public void LoadDataButton()
    {
        // TODO - Implement loading data.
        Person = new() { FirstName = "Thorsten", LastName = "Jenning", Age = 52 };

    }
    public bool CanLoadDataButton()
    {
        bool output = true;

        return output;
    }

    [RelayCommand(CanExecute = nameof(CanDiscardButton))]
    public void DiscardButton()
    {
        Person.FirstName = "Jane";
        Person.LastName = "Doe";
        Person.Age = 20;
    }
    public bool CanDiscardButton()
    {
        bool output = true;

        //if (Person.HasErrors)
        //{
        //    output = false;
        //}

        // TODO - If HasErrors is firing this check might not be necessary.
        if (Person.FirstName.Length == 0 || Person.LastName.Length == 0 || Person.Age <= 0)
        {
            output = false;
        }
        return output;
    }

}
