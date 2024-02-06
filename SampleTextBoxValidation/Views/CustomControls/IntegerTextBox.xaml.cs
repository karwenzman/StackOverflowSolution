using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleTextBoxValidation.Views.CustomControls
{
    public partial class IntegerTextBox : UserControl
    {
        [GeneratedRegex("[^0-9]+")]
        private static partial Regex IntegerPositivValuesOnlyRegex();

        public static readonly DependencyProperty DependencyPropertyOfValue =
            DependencyProperty.Register(nameof(Value), typeof(int?), typeof(IntegerTextBox), new PropertyMetadata(0));

        public int? Value
        {
            get { return (int?)GetValue(DependencyPropertyOfValue); }
            set { SetValue(DependencyPropertyOfValue, value); }
        }

        public IntegerTextBox()
        {
            InitializeComponent();

            integerTextBox.TextChanged += IntegerTextBox_TextChanged;
            integerTextBox.LostFocus += IntegerTextBox_LostFocus;
            integerTextBox.PreviewTextInput += IntegerTextBox_PreviewTextInput;
            integerTextBox.PreviewKeyDown += IntegerTextBox_PreviewKeyDown;
        }

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(IntegerTextBox_PreviewTextInput)}");

            //e.Handled = IntegerPositivValuesOnlyRegex().IsMatch(e.Text);
        }

        private void IntegerTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(IntegerTextBox_PreviewKeyDown)}");

            //if (e.Key == Key.Space)
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = false;
            //}
        }

        private void IntegerTextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(IntegerTextBox_LostFocus)}");

        }

        private void IntegerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(IntegerTextBox_TextChanged)}");

            bool valid = int.TryParse(integerTextBox.Text, out int validInteger);

            if ( valid )
            {
                Value = validInteger;
            }
            else
            {
                Value = 999;
            }
        }
    }
}
