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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CardDemo
{
    internal delegate void MyEventHandler(object parameter);
    public sealed partial class SingleCardUserControl : UserControl
    {
        internal event MyEventHandler DeleteCardListEvent;
        public SingleCardUserControl()
        {
            this.InitializeComponent();
            this.cardTitleModel = new CardTitleViewModel();
            deleteImage.Tapped += DeleteImage_Tapped;
            addImage.Tapped += AddImage_Tapped;
            cardContentListView.DragItemsCompleted += CardContentListView_DragItemsCompleted;
            cardContentListView.ContainerContentChanging += CardContentListView_ContainerContentChanging;
            
        }

        private void CardContentListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            
        }

        private void CardContentListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void AddImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(AddCardContentPage),this.cardTitleModel);
        }

        private void DeleteImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //delete
            DeleteCardListEvent(this.cardTitleModel);
        }

        public CardTitleViewModel cardTitleModel { get; set; }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            //delete
            CardContent clickVM = item.DataContext as CardContent;
            this.cardTitleModel.Contents.Remove(clickVM);
        }

        
    }
}
