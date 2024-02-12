using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleTextBoxValidation.Views.CustomControls;

/// <summary>
/// This custom control adds behavior to a class inheriting from <see cref="TextBox"/>.
/// <para></para>
/// Custom members:
/// <br></br>- a dependency property is registered and linking to the property <see cref="IntegerValue"/>
/// <br></br>- a callback method is implemented <see cref="OnIntegerValueChangedCallback(DependencyObject, DependencyPropertyChangedEventArgs)"/>
/// <br></br>- a <see cref="Regex"/> expression is defined to control user input
/// </summary>
public partial class IntegerTextBox : TextBox
{
    // My understanding is, that manually raising a property changed event is not necessary.
    // The property engine does this. Is that correct?
    public event PropertyChangedEventHandler? PropertyChanged;

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex PositiveIntergerValuesRegex();

    // Option a)
    // This is the xaml code for the registered property:
    // IntegerValue="{Binding IntValue, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, ValidatesOnNotifyDataErrors=True}" />
    // Is the setting of BindsTwoWayByDefault=true making the expression in xaml redundant, or vice versa?
    // Is the setting of DefaultUpdateSourceTrigger=PropertyChanged making the expression in xaml redundant, or vice versa?
    private static FrameworkPropertyMetadata? _frameworkPropertyMetadata = new()
    {
        PropertyChangedCallback = OnIntegerValueChangedCallback,
        BindsTwoWayByDefault = true,
        DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged,
    };

    // Option b)
    // If I just have to register a callback, then this option is sufficient.
    private static PropertyMetadata? _propertyMetadata = new()
    {
        PropertyChangedCallback = OnIntegerValueChangedCallback,
    };

    public int IntegerValue
    {
        get { return (int)GetValue(ValueProperty); }
        set
        {
            // My understanding is, that there is no need to add any additional logic into this setter.
            Debug.WriteLine($"Entered Setter in {nameof(IntegerTextBox)}");
            SetValue(ValueProperty, value);
        }
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(IntegerValue), typeof(int), typeof(IntegerTextBox), _frameworkPropertyMetadata);

    public IntegerTextBox()
    {
        TextChanged += TextBox_TextChanged;
        GotFocus += TextBox_GotFocus;
        PreviewTextInput += TextBox_PreviewTextInput;
        PreviewKeyDown += TextBox_PreviewKeyDown;
    }

    private void OnPropertyChanged(string propertyName)
    {
        Debug.WriteLine($"Entered {nameof(OnPropertyChanged)} in {nameof(IntegerTextBox)}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
    }

    private static void OnIntegerValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(OnIntegerValueChangedCallback)} in {nameof(IntegerTextBox)}");
        Debug.WriteLine($"Value changed from {(int)e.OldValue} to {(int)e.NewValue}");
        // This is my main question:
        // Does this command start the data binding so that the UI is updating the textbox?
        // I do see the correct values in NewValue and OldValue, but the UI does not display the value.
        d.SetCurrentValue(ValueProperty, (int)e.NewValue);

        // Is it necessary to update the Property to start the data binding?
        var myInstance = (IntegerTextBox)d;
        myInstance.IntegerValue = (int)e.NewValue;
        // Is calling OnPorpertyChanged manually really necessary?
        myInstance.OnPropertyChanged(nameof(IntegerValue));
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(TextBox_GotFocus)} in {nameof(IntegerTextBox)}");

        (sender as TextBox)!.SelectAll();
    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(TextBox_PreviewTextInput)} in {nameof(IntegerTextBox)}");

        e.Handled = PositiveIntergerValuesRegex().IsMatch(e.Text);
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        Debug.WriteLine($"Entered {nameof(TextBox_PreviewKeyDown)} in {nameof(IntegerTextBox)}");

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
        Debug.WriteLine($"Entered {nameof(TextBox_TextChanged)} in {nameof(IntegerTextBox)}");

        bool valid = int.TryParse((sender as TextBox)!.Text,
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out int validInteger);

        if (valid)
        {
            IntegerValue = validInteger;
        }
        else
        {
            IntegerValue = 999; // for testing, only, to show an error was not handled
        }
    }
}
