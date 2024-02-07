using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleTextBoxValidation.Views.CustomControls
{
    public partial class IntegerTextBox : UserControl
    {
        [GeneratedRegex("[^0-9]+")]
        private static partial Regex IntegerPositiveValuesOnlyRegex();

        public static readonly DependencyProperty DependencyPropertyOfValue =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(IntegerTextBox), new PropertyMetadata(0));

        public int Value
        {
            get { return (int)GetValue(DependencyPropertyOfValue); }
            set { SetValue(DependencyPropertyOfValue, value); }
        }

        public IntegerTextBox()
        {
            InitializeComponent();

            CustomIntegerTextBox.TextChanged += TextBox_TextChanged;
            CustomIntegerTextBox.GotFocus += TextBox_GotFocus;
            CustomIntegerTextBox.PreviewTextInput += TextBox_PreviewTextInput;
            CustomIntegerTextBox.PreviewKeyDown += TextBox_PreviewKeyDown;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(TextBox_GotFocus)}");

            (sender as TextBox)!.SelectAll();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(TextBox_PreviewTextInput)}");

            e.Handled = IntegerPositiveValuesOnlyRegex().IsMatch(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(TextBox_PreviewKeyDown)}");

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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(TextBox_TextChanged)}");

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
}
