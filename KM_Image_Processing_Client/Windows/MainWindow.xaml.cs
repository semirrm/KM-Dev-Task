using KM_Image_Processing_Client.Proxy;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.ComponentModel;

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
        /// <para>Constaints: Only a single .png/.jpg/.gif/.jpeg will be accepted. 
        /// All other file extentions and multiple files drop will be ignored.</para>
        ///</summary>
        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {
            if (_changes)
            {
                MessageBoxResult result = ShowDoYouWantToSaveDialog();
                if(result != MessageBoxResult.Cancel)
                {
                    if(result == MessageBoxResult.Yes)
                        SaveImage(_imagePath);
                    AddDropedImage(e);
                }
            }
            else
            {
                AddDropedImage(e);
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
                SaveImage(_imagePath);
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


            if (sDlg.FileName != "" && sDlg.ShowDialog() == true)
            {
                SaveImage(sDlg.FileName);
                AddImageToPanel(sDlg.FileName);
                _changes = false;
            }
            
        }

        ///<summary>
        /// Handles the clicking on the MenuItem - Exit.
        ///</summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ///<summary>
        /// Handles the clicking on the MenuItem and Button - Convert.
        ///</summary>
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bImg = Image.Source as BitmapImage;
            byte[] convImg = _proxy.sendImageProcessingData(ImageToStream(bImg), HorizontalFlipCheckbox.IsChecked,
                                            VerticalFlipCheckbox.IsChecked, GrayScaleCheckbox.IsChecked,
                                            EntropyCropCheckbox.IsChecked);
               

            
            Image.Source = StreamToImage(convImg);
            _changes = true;

            //Unchecks the checkboxes
            VerticalFlipCheckbox.IsChecked = false;
            HorizontalFlipCheckbox.IsChecked = false;
            GrayScaleCheckbox.IsChecked = false;
            EntropyCropCheckbox.IsChecked = false;
        }

        ///<summary>
        /// Handles the clicking on the MenuItem and Button - Convert.
        ///</summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = Window.GetWindow(this);
            aboutWindow.Show();
        }
        


        /// <summary>
        /// Handles the closing of the application. Asks if you want to save the changes.
        /// </summary>
        protected void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_changes)
                if (ShowDoYouWantToSaveDialog() != MessageBoxResult.Cancel)
                {
                    Application curApp = Application.Current;
                    curApp.Shutdown();
                }
                else
                    e.Cancel = true;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Converts byte[] into a BitmapImage
        /// </summary>
        /// <param name="imageStream">The byte[] that the BitmapImage is converted from.</param>
        /// <returns>The BitmapImage converted from the byte[].</returns>
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

        /// <summary>
        /// Converts BitmapImage into a byte[]
        /// </summary>
        /// <param name="image">The BitmapImage that the byte[] is converted from.</param>
        /// <returns>The BitmapImage converted from the byte[].</returns>
        private byte[] ImageToStream(BitmapImage image)
        {
            byte[] data;

            BitmapEncoder encoder;
            if (_imagePath.Contains(".png"))
                encoder = new PngBitmapEncoder();
            else if (_imagePath.Contains(".jpg") || _imagePath.Contains(".jpeg"))
                encoder = new JpegBitmapEncoder();
            else
                encoder = new GifBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(image));

            using(MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        /// <summary>
        /// Saves the image from Image panel to the path.
        /// </summary>
        /// <param name="newPath">The path the image is saved to.</param>
        private void SaveImage(string newPath)
        {
            _imagePath.ToLower();
            BitmapImage img = Image.Source as BitmapImage;

            BitmapEncoder encoder;
            if (_imagePath.Contains(".png"))
                encoder = new PngBitmapEncoder();
            else if (_imagePath.Contains(".jpg") || _imagePath.Contains(".jpeg"))
                encoder = new JpegBitmapEncoder();
            else
                encoder = new GifBitmapEncoder();

            using(FileStream fileStream = new FileStream(newPath, FileMode.Create))
            {
                encoder.Frames.Add(BitmapFrame.Create(img));
                encoder.Save(fileStream);
            }
        }

        /// <summary>
        /// Adds the image from a path to the image panel and enables the use of the processing options.
        /// </summary>
        /// <param name="newPath">The path to the image to be opened.</param>
        private void AddImageToPanel(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            Image.Source = bitmap;
            _imagePath = path;

            //Enables the checkboxes and the MenuItem buttons
            SaveButton.IsEnabled = true;
            SaveAsButton.IsEnabled = true;
            VerticalFlipCheckbox.IsEnabled = true;
            HorizontalFlipCheckbox.IsEnabled = true;
            GrayScaleCheckbox.IsEnabled = true;
            EntropyCropCheckbox.IsEnabled = true;

            string[] pathArr = path.Split('\\');
            this.Title = pathArr[pathArr.Length - 1] + " --- Image Processor App";
            _changes = false;
        }

        /// <summary>
        /// Opens the file picker. Filters out all the selectable files.
        /// </summary>
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

        /// <summary>
        /// Shows a MessageBox and asks the user if he wants to save the changes.
        /// </summary>
        /// <returns>A MessageBoxResult.Yes, when user clicks the "Yes" button, 
        /// a MessageBoxResult.No, when the user clicks the "No" button,
        /// and a MessageBoxResult.Cancel, when the user clicks the "Cancel" button.</returns>
        private MessageBoxResult ShowDoYouWantToSaveDialog()
        {
            MessageBoxResult mbs = MessageBox.Show(this, "Do you want to save the changes?", "Image Processor App", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            if (mbs == MessageBoxResult.Yes)
            {
                SaveImage(_imagePath);
            }
            return mbs;
        }

        /// <summary>
        /// Adds a dropped image to the ImagePanel.
        /// <para>Constaints: Only a single .png/.jpg/.gif/.jpeg will be accepted. 
        /// All other file extentions and multiple files drop will be ignored.</para>
        /// </summary>
        /// <param name="e">The DragEventArgs from the event handler.</param>
        private void AddDropedImage(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 &&
                        (files[0].ToLower().Contains(".png")
                        || files[0].ToLower().Contains(".jpg")
                        || files[0].ToLower().Contains(".gif")
                        || files[0].ToLower().Contains(".jpeg")))
                {
                    _imagePath = files[0];
                    AddImageToPanel(_imagePath);
                }
            }
        }

        #endregion
    }
}
