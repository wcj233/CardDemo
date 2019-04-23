using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CardDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.cardTitleModel = new CardTitleModel();
            this.cardContentModel = new CardContentModel();
            this.DataContext = this.cardTitleModel;
        }

        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void AddSuccess_Click(object sender, RoutedEventArgs e)
        {
            //add list view
            ObservableCollection<CardTitle> lists = new ObservableCollection<CardTitle>();
            ObservableCollection<CardContent> contentLists = new ObservableCollection<CardContent>();
            contentLists.Add(new CardContent { contentTitle = "today", contentDetail = "work"});
            lists.Add(new CardTitle { cardTitle = "to do", contents = contentLists });
            lists.Add(new CardTitle { cardTitle = "doing", contents = contentLists });
            
            this.cardTitleModel.CardTitles = lists;
            this.cardContentModel.CardContents = contentLists;
        }
        public CardTitleModel cardTitleModel { get; set; }
        public CardContentModel cardContentModel { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
