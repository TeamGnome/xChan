using LibChan;
using LibChan.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace xChan
{
    /// <summary>
    /// Interaction logic for entryWnd.xaml
    /// </summary>
    public partial class entryWnd : MetroWindow
    {
        public entryWnd()
        {
            InitializeComponent();

            siteLst.ItemsSource = ChanManager.ChanWebsites;
        }

        private void siteLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseChan bc = siteLst.SelectedValue as BaseChan;

            if (bc == null)
            {
                return;
            }

            boardLst.ItemsSource = null;
            threadLst.ItemsSource = null;

            LoadingBlock.Visibility = Visibility.Visible;

            Task t = new Task(async () =>
            {
                var boards = await bc.GetBoardsAsync();

                await Dispatcher.InvokeAsync(() =>
                {
                    boardLst.ItemsSource = boards;
                    boardLst.ScrollIntoView(boardLst.Items[0]);

                    (controlTabs.Items[1] as TabItem).Visibility = Visibility.Visible;
                    controlTabs.SelectedIndex = 1;

                    LoadingBlock.Visibility = Visibility.Hidden;
                    updateBreadCrumbs(bc.DisplayName);
                });
            });

            t.Start();
        }

        private void boardLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseChan bc = siteLst.SelectedValue as BaseChan;
            ChanBoard cb = boardLst.SelectedValue as ChanBoard;

            if (bc == null || cb == null)
            {
                return;
            }

            threadLst.ItemsSource = null;

            LoadingBlock.Visibility = Visibility.Visible;

            Task t = new Task(async () =>
            {
                var threads = await bc.GetCatalogForBoardAsync(cb.UrlSlug);

                if (threads == null || threads.Count() == 0)
                {
                    return;
                }

                await Dispatcher.InvokeAsync(() =>
                {
                    threadLst.ItemsSource = threads.OrderByDescending(th => th.Posts);
                    threadLst.ScrollIntoView(threadLst.Items[0]);

                    (controlTabs.Items[2] as TabItem).Visibility = Visibility.Visible;
                    controlTabs.SelectedIndex = 2;

                    LoadingBlock.Visibility = Visibility.Hidden;
                    updateBreadCrumbs(bc.DisplayName, cb.Title);
                });
            });

            t.Start();
        }

        private void threadLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseChan bc = siteLst.SelectedValue as BaseChan;
            ChanBoard cb = boardLst.SelectedValue as ChanBoard;
            ChanCatalogThread cct = threadLst.SelectedValue as ChanCatalogThread;

            if (bc == null || cb == null || cct == null)
            {
                return;
            }

            imageLst.ItemsSource = null;

            LoadingBlock.Visibility = Visibility.Visible;


            Task t = new Task(async () =>
            {
                var posts = await bc.GetPostsForTheadAsync(cct.BoardSlug, (int)cct.ThreadId);

                if (posts == null || posts.Count() == 0)
                {
                    return;
                }


                await Dispatcher.InvokeAsync(() =>
                {
                    imageLst.ItemsSource = posts.Where(m => m.Files != null).SelectMany(m => m.Files);
                    if (imageLst.Items.Count > 0)
                    {
                        imageLst.ScrollIntoView(imageLst.Items[0]);
                    }

                    (controlTabs.Items[3] as TabItem).Visibility = Visibility.Visible;
                    controlTabs.SelectedIndex = 3;

                    LoadingBlock.Visibility = Visibility.Hidden;
                    updateBreadCrumbs(bc.DisplayName, cb.Title, string.Format("{0} - {1}", cct.ThreadId, cct.SubjectSafe));
                });
            });

            t.Start();
        }

        private void controlTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Source is TabControl))
            {
                // we only want to handle user navigation, not selection changes prompted by other controls
                return;
            }

            int start = controlTabs.SelectedIndex + 1;

            if (start == controlTabs.Items.Count)
            {
                // the last tab has been selected
                return;
            }

            for (; start < controlTabs.Items.Count; start++)
            {
                (controlTabs.Items[start] as TabItem).Visibility = Visibility.Hidden;
            }

            switch (controlTabs.SelectedIndex)
            {
                case 0:
                    siteLst.SelectedIndex = -1;
                    updateBreadCrumbs();
                    break;
                case 1:
                    boardLst.SelectedIndex = -1;
                    updateBreadCrumbs(SiteCrumb.Text.TrimEnd(new [] { ' ', '>' }));
                    break;
                case 2:
                    threadLst.SelectedIndex = -1;
                    updateBreadCrumbs(SiteCrumb.Text.TrimEnd(new[] { ' ', '>' }), BoardCrumb.Text.TrimEnd(new[] { ' ', '>' }));
                    break;
                case 3:
                    imageLst.SelectedIndex = -1;
                    break;
            }
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

        private void imageDownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists("DUMP"))
            {
                Directory.CreateDirectory("DUMP");
            }

            using (WebClient wc = new WebClient())
            {
                foreach (ChanPostFile item in imageLst.SelectedItems)
                {
                    wc.DownloadFile(item.Uri, Path.Combine(@"DUMP", string.Format("{0}{1}", item.MD5String, item.Extension)));
                }
            }
        }

        private void updateBreadCrumbs(string site = "", string board = "", string thread = "")
        {
            bool setBoldPart = false;

            if (string.IsNullOrEmpty(thread))
            {
                ThreadCrumb.FontWeight = FontWeight.FromOpenTypeWeight(400);
                ThreadCrumb.Visibility = Visibility.Hidden;
            }
            else
            {
                ThreadCrumb.FontWeight = FontWeight.FromOpenTypeWeight(700);
                ThreadCrumb.Visibility = Visibility.Visible;
                ThreadCrumb.Text = thread;
                setBoldPart = true;
            }

            if (string.IsNullOrEmpty(board))
            {
                BoardCrumb.FontWeight = FontWeight.FromOpenTypeWeight(400);
                BoardCrumb.Visibility = Visibility.Hidden;
            }
            else
            {
                BoardCrumb.FontWeight = FontWeight.FromOpenTypeWeight(setBoldPart ? 400 : 700);
                BoardCrumb.Visibility = Visibility.Visible;
                BoardCrumb.Text = board;
                if (setBoldPart)
                {
                    BoardCrumb.Text += " >";
                }
                setBoldPart = true;
            }

            if (string.IsNullOrEmpty(site))
            {
                SiteCrumb.FontWeight = FontWeight.FromOpenTypeWeight(400);
                SiteCrumb.Visibility = Visibility.Hidden;
            }
            else
            {
                SiteCrumb.FontWeight = FontWeight.FromOpenTypeWeight(setBoldPart ? 400 : 700);
                SiteCrumb.Visibility = Visibility.Visible;
                SiteCrumb.Text = site;
                if (setBoldPart)
                {
                    SiteCrumb.Text += " >";
                }
                setBoldPart = true;
            }
        }
    }
}
