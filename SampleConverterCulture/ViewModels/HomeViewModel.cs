using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace SampleConverterCulture.ViewModels;

public partial class HomeViewModel : ViewModelBase, IHomeViewModel
{
	[ObservableProperty]
	private ReceiptModel _receipt = new();
	private ReceiptModel _backupReceipt = new();

	[ObservableProperty]
	private string _screenTitle = "ksjalkjd";

	[ObservableProperty]
	private string _screenFooter = "ksjalkjd";

	public HomeViewModel()
	{
		Receipt.ReceiptId = 1;
		Receipt.ReceiptDate = DateOnly.FromDateTime(DateTime.Today);
		Receipt.ReceiptName = "Dummy Yummy Receipt";
		Receipt.ReceiptPrice = 1.11M;

		_backupReceipt.ReceiptId = 1;
		_backupReceipt.ReceiptDate = DateOnly.FromDateTime(DateTime.Today);
		_backupReceipt.ReceiptName = "Dummy Yummy Receipt";
		_backupReceipt.ReceiptPrice = 1.11M;

		Receipt.PropertyChanged += Receipt_PropertyChanged;
	}

	private void Receipt_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		DiscardButtonCommand.NotifyCanExecuteChanged();
		SaveDataButtonCommand.NotifyCanExecuteChanged();
	}

	[RelayCommand(CanExecute = nameof(CanDiscardButton))]
	public void DiscardButton()
	{
		Receipt.ReceiptId = _backupReceipt.ReceiptId;
		Receipt.ReceiptDate = _backupReceipt.ReceiptDate;
		Receipt.ReceiptName = _backupReceipt.ReceiptName;
		Receipt.ReceiptPrice = _backupReceipt.ReceiptPrice;
	}
	public bool CanDiscardButton()
	{
		if (Receipt.ReceiptId != _backupReceipt.ReceiptId)
		{
			return true;
		}
		if (Receipt.ReceiptDate != _backupReceipt.ReceiptDate)
		{
			return true;
		}
		if (Receipt.ReceiptName != _backupReceipt.ReceiptName)
		{
			return true;
		}
		if (Receipt.ReceiptPrice != _backupReceipt.ReceiptPrice)
		{
			return true;
		}

		return false;
	}

	[RelayCommand]
	public void LoadDataButton()
	{
		Receipt.ReceiptId = 9999;
		Receipt.ReceiptDate = DateOnly.FromDateTime(DateTime.Today);
		Receipt.ReceiptName = "Hotel and Beverage Receipt";
		Receipt.ReceiptPrice = 9999.99M;

		_backupReceipt.ReceiptId = 9999;
		_backupReceipt.ReceiptDate = DateOnly.FromDateTime(DateTime.Today);
		_backupReceipt.ReceiptName = "Hotel and Beverage Receipt";
		_backupReceipt.ReceiptPrice = 9999.99M;

		DiscardButtonCommand.NotifyCanExecuteChanged();
		MessageBox.Show("New Values loaded.", "LoadButton");
	}

	[RelayCommand(CanExecute = nameof(CanSaveDataButton))]
	public void SaveDataButton()
	{
		_backupReceipt.ReceiptId = Receipt.ReceiptId;
		_backupReceipt.ReceiptDate = Receipt.ReceiptDate;
		_backupReceipt.ReceiptName = Receipt.ReceiptName;
		_backupReceipt.ReceiptPrice = Receipt.ReceiptPrice;

		DiscardButtonCommand.NotifyCanExecuteChanged();
		MessageBox.Show("Values moved to private backup fields.", "SaveButton");
	}
	public bool CanSaveDataButton()
	{
		return !HasErrors;
	}

}
