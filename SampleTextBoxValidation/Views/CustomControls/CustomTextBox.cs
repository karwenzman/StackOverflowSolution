using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleTextBoxValidation.Views.CustomControls;

public partial class CustomTextBox : TextBox
{
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex IntegerPositiveValuesOnlyRegex();

    private static FrameworkPropertyMetadata? _frameworkPropertyMetadata = new() { PropertyChangedCallback = ValueChangedCallback, BindsTwoWayByDefault = true };
    private static PropertyMetadata? _propertyMetadata = new() { PropertyChangedCallback = ValueChangedCallback };
    
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(int), typeof(CustomTextBox), _frameworkPropertyMetadata);

    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    public CustomTextBox()
    {
        TextChanged += TextBox_TextChanged;
        GotFocus += TextBox_GotFocus;
        PreviewTextInput += TextBox_PreviewTextInput;
        PreviewKeyDown += TextBox_PreviewKeyDown;
    }

    private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(ValueChangedCallback)} in {nameof(CustomTextBox)}");
        d.SetValue(ValueProperty, (int)e.NewValue);
        Debug.WriteLine($"Value is: {(int)e.NewValue}");

    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(TextBox_GotFocus)} in {nameof(CustomTextBox)}");

        (sender as TextBox)!.SelectAll();
    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(TextBox_PreviewTextInput)} in {nameof(CustomTextBox)}");

        e.Handled = IntegerPositiveValuesOnlyRegex().IsMatch(e.Text);
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(TextBox_PreviewKeyDown)} in {nameof(CustomTextBox)}");

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
        Debug.WriteLine($"Entered {nameof(TextBox_TextChanged)} in {nameof(CustomTextBox)}");

        bool valid = int.TryParse((sender as TextBox)!.Text,
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out int validInteger);

        if (valid)
        {
            Value = validInteger;
        }
        else
        {
            Value = 999; // for testing, only, to show an error was not handled
        }
    }
}
