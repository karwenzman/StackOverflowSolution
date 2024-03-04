using System.Globalization;

namespace SampleConverterCulture.Models;

/// <summary>
/// Source: https://www.codeproject.com/tips/1004834/binding-with-respect-to-currentculture
/// the final step is what i dont understand
/// </summary>
public class CustomBinding : System.Windows.Data.Binding
{
	public CustomBinding()
	{
		ConverterCulture = CultureInfo.CurrentCulture;
		//ConverterCulture = Thread.CurrentThread.CurrentCulture;
	}
}
