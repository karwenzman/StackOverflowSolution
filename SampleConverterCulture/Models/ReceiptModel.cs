using CommunityToolkit.Mvvm.ComponentModel;

namespace SampleConverterCulture.Models;

public partial class ReceiptModel : ModelBase
{
	[ObservableProperty]
	private int? _receiptId;

	[ObservableProperty]
	private string? _receiptName;

	[ObservableProperty]
	private decimal? _receiptPrice;

	[ObservableProperty]
	private DateOnly? _receiptDate;
}
