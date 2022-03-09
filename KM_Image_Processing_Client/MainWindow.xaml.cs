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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private bool _IsChecked_HorizontalFlip;
        private bool _IsChecked_VerticalFlip;
        private bool _IsChecked_GrayScale;
        private bool _IsChecked_EntropyCrop;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        public bool IsChecked_HorizontalFlip
        {
            get { return _IsChecked_HorizontalFlip; }
            set
            {
                if (value == true)
                    ConvertButton.IsEnabled = true;

                _IsChecked_HorizontalFlip = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsChecked_VerticalFlip
        {
            get { return _IsChecked_VerticalFlip; }
            set
            {
                if (value == true)
                    ConvertButton.IsEnabled = true;

                _IsChecked_VerticalFlip = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsChecked_GrayScale
        {
            get { return _IsChecked_GrayScale; }
            set
            {
                if (value == true)
                    ConvertButton.IsEnabled = true;

                _IsChecked_GrayScale = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsChecked_EntropyCroped
        {
            get { return _IsChecked_EntropyCrop; }
            set
            {
                if (value == true)
                    ConvertButton.IsEnabled = true;

                _IsChecked_EntropyCrop = value;
                NotifyPropertyChanged();
            }
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
    }
}
