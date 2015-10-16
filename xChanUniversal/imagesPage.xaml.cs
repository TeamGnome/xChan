using LibChan;
using LibChan.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace xChan
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class imagesPage : Page
   {
      private BaseChan chanSite { get; set; }
      private ChanBoard chanBoard { get; set; }
      private ChanCatalogThread chanThread { get; set; }

      public imagesPage()
      {
         this.InitializeComponent();
      }
      protected override async void OnNavigatedTo(NavigationEventArgs e)
      {
         base.OnNavigatedTo(e);

         var navData = e.Parameter as Tuple<BaseChan, ChanBoard, ChanCatalogThread>;
         chanSite = navData.Item1;
         chanBoard = navData.Item2;
         chanThread = navData.Item3;

         if (chanSite != null)
         {
            var src = await chanSite.GetPostsForTheadAsync(chanBoard.UrlSlug, (int)chanThread.ThreadId);
            imageLst.ItemsSource = src.Where(m => m.Files != null).SelectMany(m => m.Files);
         }
      }
   }
}
