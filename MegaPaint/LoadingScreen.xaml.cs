using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;

namespace MegaPaint
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        Thread loadingThread;
        Storyboard Showboard;
        Storyboard Hideboard;
        private delegate void ShowDelegate(string txt);
        private delegate void HideDelegate();
        ShowDelegate showDelegate;
        HideDelegate hideDelegate;

        public LoadingScreen()
        {
            InitializeComponent();
            showDelegate = new ShowDelegate(this.showText);
            hideDelegate = new HideDelegate(this.hideText);
            Showboard = this.Resources["showStoryBoard"] as Storyboard;
            Hideboard = this.Resources["HideStoryBoard"] as Storyboard;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadingThread = new Thread(load);
            loadingThread.Start();
        }
        private void load()
        {
            Thread.Sleep(1000);
            this.Dispatcher.Invoke(showDelegate, "LÊ VIẾT TOÀN");
            Thread.Sleep(2000);
            //load data 
            this.Dispatcher.Invoke(hideDelegate);

            Thread.Sleep(1000);
            this.Dispatcher.Invoke(showDelegate, "1312604");
            Thread.Sleep(2000);
            //load data
            this.Dispatcher.Invoke(hideDelegate);

            //close the window
            Thread.Sleep(1000);
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
(Action)delegate () { Close(); });
        }
        private void showText(string txt)
        {
            txtLoading.Text = txt;
            BeginStoryboard(Showboard);
        }
        private void hideText()
        {
            BeginStoryboard(Hideboard);
        }
    }
}
