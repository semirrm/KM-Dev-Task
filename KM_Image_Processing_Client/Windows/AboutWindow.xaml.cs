using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KM_Image_Processing_Client
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        #region Constructor
        public AboutWindow()
        {
            InitializeComponent();
            PopulateAboutWindow();
        }
        #endregion

        #region Event handlers

        /// <summary>
        /// Handles the click of the button - ok.
        /// </summary>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Private functions
        /// <summary>
        /// Populates window withe the information from the assembly.
        /// </summary>
        private void PopulateAboutWindow()
        {
            AssemblyInfo info = new AssemblyInfo(Assembly.GetEntryAssembly());

            AssemblyTitle.Content = info.ProductTitle;
            AssemblyDescription.Content = info.Description;
            AssemblyAuthor.Content = info.Company;
            AssemblyCopyright.Content = info.Copyright;
            AssemblyVersion.Content = "Version: " + info.Version;
            
        }
        #endregion
    }
}
