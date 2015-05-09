using MahApps.Metro.Controls;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace xChan
{
    /// <summary>
    /// Interaction logic for settingWnd.xaml
    /// </summary>
    public partial class settingWnd : MetroWindow
    {
        public settingWnd()
        {
            InitializeComponent();
        }

        private void selectFolder(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderTxt.Text = fbd.SelectedPath;
            }
        }

        private void MetroWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if(!Directory.Exists(FolderTxt.Text))
            {
                FolderTxt.Text = "";
            }

            Properties.Settings.Default.Save();
        }
    }
}
