using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KM_Image_Processing_Client
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

        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && 
                        (files[0].ToLower().Contains(".png") 
                        || files[0].ToLower().Contains(".jpg")
                        || files[0].ToLower().Contains(".gif")
                        || files[0].ToLower().Contains(".jpeg"))) { 
                    Image.Source = new BitmapImage(new Uri(files[0]));
                    SaveButton.IsEnabled = true;
                    SaveAsButton.IsEnabled = true;
                    VerticalFlipCheckbox.IsEnabled = true;
                    HorizontalFlipCheckbox.IsEnabled = true;
                    GrayScaleCheckbox.IsEnabled = true;
                    EntropyCropCheckbox.IsEnabled = true;
                }
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
