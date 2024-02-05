using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleTextBoxValidation.Views.CustomControls
{
    public partial class IntegerTextBox : UserControl
    {
        public IntegerTextBox()
        {
            InitializeComponent();
        }

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Debug.WriteLine($"Entered {nameof(IntegerTextBox_PreviewTextInput)}");

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
    }
}
