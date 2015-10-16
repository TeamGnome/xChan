using LibChan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace xChan
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      public MainPage()
      {
         this.InitializeComponent();
      }

      private void Grid_Loaded(object sender, RoutedEventArgs e)
      {
         siteLst.ItemsSource = ChanManager.ChanWebsites;
      }

      private void siteLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         BaseChan bc = siteLst.SelectedValue as BaseChan;
         if (bc == null) return;

         Frame.Navigate(typeof(boardPage), bc);
      }
   }
}
