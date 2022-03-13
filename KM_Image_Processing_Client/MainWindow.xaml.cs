﻿using KM_Image_Processing_Client.Proxy;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace KM_Image_Processing_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _imagePath { get; set; }

        private bool _changes { get; set; }

        private ServiceProxy _proxy { get; set; }

        #region constructor

        public MainWindow()
        {
            InitializeComponent();
            _changes = false;
            _proxy = new ServiceProxy();
        }

        #endregion

        #region Event Handlers

        ///<summary>
        /// Handling of the drag and drop functionality.
        /// 
        /// <para>Constaints: Only a single .png/.jpg/.gif/.jpeg will be accepted. 
        /// All other file extentions and multiple files drop will be ignored.</para>
        ///</summary>
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
                    _imagePath = files[0];
                    AddImageToPanel(_imagePath);
                }
            }
        }


        ///<summary>
        /// Handles the clicking on the MenuItem - Open.
        ///</summary>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (_changes)
            {
                if (ShowDoYouWantToSaveDialog() != MessageBoxResult.Cancel)
                    ShowOpenFileDialog();
            }
            else
                ShowOpenFileDialog();

        }

        ///<summary>
        /// Handles the clicking on the MenuItem - Save.
        ///</summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_changes)
                SaveImage(_imagePath, _imagePath);
            _changes = false;
        }

        ///<summary>
        /// Handles the clicking on the MenuItem - Save As.
        ///</summary>
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.Title = "Save Image";

            string[] path = _imagePath.Split('\\');
            string dir = "";
            for (int i = 0; i < path.Length - 1; i++)
                dir += path[i] + "\\";
            sDlg.InitialDirectory = dir;
            sDlg.FileName = path[path.Length - 1];

            string extention = _imagePath.Split('.')[1];
            sDlg.Filter = extention.ToUpper() + " Files (*." + extention + ")|*." + extention;

            sDlg.ShowDialog();
            if (sDlg.FileName != "")
            {
                SaveImage(_imagePath, sDlg.FileName);
            }
            _changes = false;
        }

        ///<summary>
        /// Handles the clicking on the MenuItem - Exit.
        ///</summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (_changes)
                if (ShowDoYouWantToSaveDialog() != MessageBoxResult.Cancel) {
                    Application curApp = Application.Current;
                    curApp.Shutdown();
                }
        }

        ///<summary>
        /// Handles the clicking on the MenuItem and Button - Convert.
        ///</summary>
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            byte[] imgArray = File.ReadAllBytes(_imagePath); //the image in view instead of the image in path
            byte[] convImg = _proxy.sendImageProcessingData(imgArray, HorizontalFlipCheckbox.IsChecked,
                                            VerticalFlipCheckbox.IsChecked, GrayScaleCheckbox.IsChecked,
                                            EntropyCropCheckbox.IsChecked);

            
            Image.Source = StreamToImage(convImg);
            _changes = true;
        }

        #endregion

        #region Private classes

        
        private BitmapImage StreamToImage(byte[] imageStream)
        {
            MemoryStream stream = new MemoryStream(imageStream);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        private Stream ImageToStream(String path)
        {
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(_imagePath));
            return ms;
        }

        private void SaveImage(String path, String newPath)
        {
            path.ToLower();
            BitmapImage img = Image.Source as BitmapImage;

            BitmapEncoder encoder;
            if (path.Contains(".png"))
                encoder = new PngBitmapEncoder();
            else if (path.Contains(".jpg") || path.Contains(".jpeg"))
                encoder = new JpegBitmapEncoder();
            else
                encoder = new GifBitmapEncoder();

            try
            {
                File.Copy(path, newPath, true);
            }
            catch(IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        private void AddImageToPanel(string path)
        {
            Image.Source = new BitmapImage(new Uri(path));

            //Enables the checkboxes and the MenuItem buttons
            SaveButton.IsEnabled = true;
            SaveAsButton.IsEnabled = true;
            VerticalFlipCheckbox.IsEnabled = true;
            HorizontalFlipCheckbox.IsEnabled = true;
            GrayScaleCheckbox.IsEnabled = true;
            EntropyCropCheckbox.IsEnabled = true;

            _changes = false;
        }

        private void ShowOpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Images (*.jpeg; *.png; *.jpg; *.gif)|*.jpeg; *.png; *.jpg; *.gif | JPEG Files (*.jpeg)|*.jpeg " +
                         "| PNG Files (*.png)|*.png | JPG Files (*.jpg)|*.jpg | GIF Files (*.gif)|*.gif";
            dlg.Title = "Select Image";


            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                _imagePath = dlg.FileName.ToLower();
                AddImageToPanel(_imagePath);
            }
        }

        private MessageBoxResult ShowDoYouWantToSaveDialog()
        {
            MessageBoxResult mbs = MessageBox.Show(this, "Do you want to save the changes?", "Image Processor App", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            if (mbs == MessageBoxResult.Yes)
            {
                SaveImage(_imagePath, _imagePath);
            }
            return mbs;
        }
        #endregion
    }
}
