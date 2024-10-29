using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCommunityToolkit.ViewModels;

public partial class HomeViewModel : ObservableObject
{
	[ObservableProperty]
	private string? _title;
	[ObservableProperty]
	private string? _description;
	[ObservableProperty]
	private string? _name;
	[ObservableProperty]
	private string? _url;
	[ObservableProperty]
	private string? _copyright;
	public HomeViewModel()
    {
        Title = "Hello World App";
		
    }
}
