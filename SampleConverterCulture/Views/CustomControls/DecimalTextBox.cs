using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleConverterCulture.Views.CustomControls;

/// <summary>
/// This class adds custom behavior to a <see cref="TextBox"/>.
/// <para></para>
/// Custom members:
/// <br></br>- a dependency property is registered and linking to the property <b>Value</b>
/// <br></br>- a callback method <see cref="ValueChangedCallback(DependencyObject, DependencyPropertyChangedEventArgs)"/>
/// <br></br>- a 
/// </summary>
public partial class DecimalTextBox : TextBox
{
	[GeneratedRegex("[^0-9.]+")]
	private static partial Regex OnlyPositiveDecimalValuesRegex();

	public static readonly FrameworkPropertyMetadata _frameworkPropertyMetadata = new()
	{
		PropertyChangedCallback = OnValueChangedCallback,
	};

	private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		// TODO - How to implement this properly?
		Debug.WriteLine($"Entered {nameof(OnValueChangedCallback)} in {nameof(DecimalTextBox)}");
		d.SetCurrentValue(ValueProperty, (decimal)e.NewValue);
	}

	public static readonly DependencyProperty ValueProperty =
		DependencyProperty.Register("Value", typeof(decimal), typeof(DecimalTextBox), _frameworkPropertyMetadata);


	public decimal Value
	{
		get { return (decimal)GetValue(ValueProperty); }
		set { SetValue(ValueProperty, value); }
	}

	public DecimalTextBox()
	{
		TextChanged += TextBox_TextChanged;
		GotFocus += TextBox_GotFocus;
		PreviewTextInput += TextBox_PreviewTextInput;
		PreviewKeyDown += TextBox_PreviewKeyDown;
	}

	private void TextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		Debug.WriteLine($"Entered {nameof(TextBox_GotFocus)} in {nameof(DecimalTextBox)}");

		(sender as TextBox)!.SelectAll();
	}

	private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		Debug.WriteLine($"Entered {nameof(TextBox_PreviewTextInput)} in {nameof(DecimalTextBox)}");

		e.Handled = OnlyPositiveDecimalValuesRegex().IsMatch(e.Text);
	}

	private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		Debug.WriteLine($"Entered {nameof(TextBox_PreviewKeyDown)} in {nameof(DecimalTextBox)}");

		if (e.Key == Key.Space)
		{
			e.Handled = true;
		}
		else if (e.Key == Key.Back)
		{
			if ((sender as TextBox)!.Text.Length == 1)
			{
				(sender as TextBox)!.SelectAll();
				e.Handled = true;
			}
		}
		else if (e.Key == Key.Delete)
		{
			if ((sender as TextBox)!.Text.Length == 1)
			{
				(sender as TextBox)!.SelectAll();
				e.Handled = true;
			}
		}
		else if (e.Key == Key.D0)
		{
			if ((sender as TextBox)!.Text.Length == 1)
			{
				if ((sender as TextBox)!.Text == "0")
				{
					(sender as TextBox)!.SelectAll();
					e.Handled = true;
				}
			}
		}
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		Debug.WriteLine($"Entered {nameof(TextBox_TextChanged)} in {nameof(DecimalTextBox)}");

		bool valid = decimal.TryParse((sender as TextBox)!.Text,
			NumberStyles.AllowDecimalPoint | NumberStyles.Integer,
			CultureInfo.InvariantCulture,
			out decimal validDecimal);

		if (valid)
		{
			Value = validDecimal;
		}
		else
		{
			Value = 999.99m; // for testing, only, to show an error was not handled
		}
	}

}
