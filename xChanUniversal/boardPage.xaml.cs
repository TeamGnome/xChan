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
   public sealed partial class boardPage : Page
   {
      private BaseChan chanSite { get; set; }

      public boardPage()
      {
         this.InitializeComponent();
      }

      protected override async void OnNavigatedTo(NavigationEventArgs e)
      {
         base.OnNavigatedTo(e);

         chanSite = e.Parameter as BaseChan;
         if(chanSite != null)
         {
            var src = await chanSite.GetBoardsAsync();
            boardLst.ItemsSource = src;
         }
      }

      private void boardLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         ChanBoard chanBoard = boardLst.SelectedValue as ChanBoard;
         if (chanBoard == null) return;

         Frame.Navigate(typeof(threadPage), new Tuple<BaseChan, ChanBoard>(chanSite, chanBoard));
      }
   }
}
