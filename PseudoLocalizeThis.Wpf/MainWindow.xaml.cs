using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PseudoLocalizeThis.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OriginalElements_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (TransformedElements.Items.Count <= 0)
            {
                return;
            }

            var scroll = VisualTreeHelper.GetChild(TransformedElements, 0) as ScrollViewer;
            if (scroll == null)
            {
                return;
            }

            scroll.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void TransformedElements_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (OriginalElements.Items.Count <= 0)
            {
                return;
            }

            var scroll = VisualTreeHelper.GetChild(OriginalElements, 0) as ScrollViewer;
            if (scroll == null)
            {
                return;
            }

            scroll.ScrollToVerticalOffset(e.VerticalOffset);
        }
    }
}