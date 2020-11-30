using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DocumentEditor
{
    internal sealed partial class MainWindow : Window
    {
        TextPointer startIndexPointer, endIndexPointer;
        int startIndex = 0, endIndex = 0;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            GetSelectionText().ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            GetSelectionText().ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            GetSelectionText().ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
        }

        private TextSelection GetSelectionText()
        {
            startIndexPointer = richTextBox.GetTextPointerAtOffset(startIndex);
            endIndexPointer = richTextBox.GetTextPointerAtOffset(endIndex);
            richTextBox.Selection.Select(startIndexPointer, endIndexPointer);
            return richTextBox.Selection;
        }

        private void fontSizeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (textRun != null && fontSizeComboBox.SelectedIndex != -1)
            {
                double size = double.Parse(((ComboBoxItem)fontSizeComboBox.SelectedItem).Content.ToString());
                GetSelectionText().ApplyPropertyValue(Run.FontSizeProperty, size);
            }
        }

        private void colorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (textRun != null && colorComboBox.SelectedIndex != -1)
            {
                Color color = Colors.White;
                switch (colorComboBox.SelectedIndex)
                {
                    case 0:
                        color = Colors.Black;
                        break;
                    case 1:
                        color = Colors.Blue;
                        break;
                    case 2:
                        color = Colors.Green;
                        break;
                    case 3:
                        color = Colors.Orange;
                        break;
                    case 4:
                        color = Colors.Purple;
                        break;
                    case 5:
                        color = Colors.Red;
                        break;
                    case 6:
                        color = Colors.Yellow;
                        break;
                    default:
                        break;
                }

                GetSelectionText().ApplyPropertyValue(Run.ForegroundProperty, new SolidColorBrush(color));
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            GetSelectionText().ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            GetSelectionText().ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            GetSelectionText().ApplyPropertyValue(Run.TextDecorationsProperty, null);
            GetSelectionText().ApplyPropertyValue(RichTextBox.FontSizeProperty, 12.0);
            GetSelectionText().ApplyPropertyValue(Run.ForegroundProperty, new SolidColorBrush(Colors.Black));
        }

        private void endTexBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textRun == null)
                return;
            string line2 = endTexBox.Text.Trim();
            if (int.TryParse(line2, out int endPos) && endPos >= startIndex && endPos < textRun.Text.Length - 1)
            {
                this.endIndex = endPos + 1;
                ActivateOrDeactivate(true);
            }           
            else
            {
                ActivateOrDeactivate(false);
            }
        }

        private void startTexBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (textRun == null)
                return;


            string line1 = startTexBox.Text;

            if (int.TryParse(line1, out int startPos) && startPos >= 0 && startPos <= endIndex - 1)
            {
                this.startIndex = startPos;
                ActivateOrDeactivate(true);
            }            
            else
            {
                ActivateOrDeactivate(false);
            }
              
        }

        private void ActivateOrDeactivate(bool tb)
        {
            toolBar1.IsEnabled = tb;
            toolBar2.IsEnabled = tb;
            toolBar3.IsEnabled = tb;
        }

    }
}