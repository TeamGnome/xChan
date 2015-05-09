using LibChan.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace xChan
{
    /// <summary>
    /// Interaction logic for previewWnd.xaml
    /// </summary>
    public partial class previewWnd : MetroWindow
    {
        public previewWnd()
        {
            InitializeComponent();
        }

        private void setTitle(object sender, RoutedEventArgs e)
        {
            var data = DataContext as ChanPostFile;
            Title = string.Format("xChan: {0}{1} ({2}x{3}, {4})", data.Original, data.Extension, data.Width, data.Height, data.SizeWithUnit);

            // also setup loading event handler for image
            (ImagePanel.Source as BitmapFrame).DownloadCompleted += PreviewWnd_DownloadCompleted;
        }

        private void PreviewWnd_DownloadCompleted(object sender, EventArgs e)
        {
            Grid.SetRowSpan(ImagePanel, 2);
            LoadingBlock.Visibility = Visibility.Collapsed;
        }

        private void saveImage(object sender, RoutedEventArgs e)
        {
            var data = DataContext as ChanPostFile;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save this image...";
            sfd.ValidateNames = true;
            sfd.Filter = string.Format("Image file|*{0}", data.Extension);
            sfd.FileName = string.Format("{0}{1}", data.Name, data.Extension);

            var result = sfd.ShowDialog(this);

            if (result.HasValue && result.Value)
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(data.Uri, sfd.FileName);
                }
            }
        }
    }
}
