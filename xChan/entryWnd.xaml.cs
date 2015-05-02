using LibChan;
using LibChan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for entryWnd.xaml
    /// </summary>
    public partial class entryWnd : Window
    {
        public entryWnd()
        {
            InitializeComponent();

            siteLst.ItemsSource = ChanManager.ChanWebsites;
        }

        private async void siteLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseChan bc = siteLst.SelectedValue as BaseChan;

            if (bc == null)
            {
                return;
            }

            boardLst.ItemsSource = null;
            threadLst.ItemsSource = null;

            boardLst.ItemsSource = await bc.GetBoardsAsync();
            boardLst.ScrollIntoView(boardLst.Items[0]);
            controlTabs.SelectedIndex = 1;
        }

        private async void boardLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseChan bc = siteLst.SelectedValue as BaseChan;
            ChanBoard cb = boardLst.SelectedValue as ChanBoard;

            if (bc == null || cb == null)
            {
                return;
            }

            threadLst.ItemsSource = null;

            var threads = await bc.GetCatalogForBoardAsync(cb.UrlSlug);

            if (threads == null || threads.Count() == 0)
            {
                return;
            }

            threadLst.ItemsSource = threads.OrderByDescending(t => t.Posts);
            threadLst.ScrollIntoView(threadLst.Items[0]);
            controlTabs.SelectedIndex = 2;
        }

        private async void threadLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseChan bc = siteLst.SelectedValue as BaseChan;
            ChanBoard cb = boardLst.SelectedValue as ChanBoard;
            ChanCatalogThread cct = threadLst.SelectedValue as ChanCatalogThread;

            if (bc == null || cb == null || cct == null)
            {
                return;
            }

            imageLst.ItemsSource = null;

            var posts = await bc.GetPostsForTheadAsync(cct.BoardSlug, (int)cct.ThreadId);

            if (posts == null || posts.Count() == 0)
            {
                return;
            }

            imageLst.ItemsSource = posts.Where(m => m.Files != null).SelectMany(m => m.Files);
            if (imageLst.Items.Count > 0)
            {
                imageLst.ScrollIntoView(imageLst.Items[0]);
            }
            controlTabs.SelectedIndex = 3;
        }

        private void downloadThreadClick(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            var dc = mi.DataContext;
            return;
        }

        private void itemOpen(object sender, RoutedEventArgs e)
        {

        }
    }
}
