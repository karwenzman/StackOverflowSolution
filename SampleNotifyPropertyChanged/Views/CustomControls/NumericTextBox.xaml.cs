using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleNotifyPropertyChanged.Views.CustomControls;

public partial class NumericTextBox : UserControl
{
    public static readonly DependencyProperty NumericStringProperty =
    DependencyProperty.Register(nameof(NumericString), typeof(int), typeof(NumericTextBox));

    public int NumericString
    {
        get { return (int)GetValue(NumericStringProperty); }
        set { SetValue(NumericStringProperty, value); }
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();

    public NumericTextBox()
    {
        InitializeComponent();
    }

    private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = MyRegex().IsMatch(e.Text);
    }

    private void NumericTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
        else
        {
            e.Handled = false;
        }
    }
}
