using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Xaml.Media.Animation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CardDemo
{
    internal delegate void MyEventHandler(object parameter);
    internal delegate void ListViewChangeItemEventHandler(CardTitleViewModel exchangeCardTitleVM,CardContent exchangeContent);
    public sealed partial class SingleCardUserControl : UserControl
    {
        
        internal event MyEventHandler DeleteCardListEvent;
        internal event ListViewChangeItemEventHandler ListViewChangeItemEvent;
        public SingleCardUserControl()
        {
            this.InitializeComponent();
            this.cardTitleVM = new CardTitleViewModel();
            deleteImage.Tapped += DeleteImage_Tapped;
            addImage.Tapped += AddImage_Tapped;
            cardContentListView.ContainerContentChanging += CardContentListView_ContainerContentChanging;
        }

        private void CardContentListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            
        }

        private void CardContentListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            CardContent dragCardContent = e.Items[0] as CardContent;
            CardTitleViewModel originalCardTitleVM = this.cardTitleVM;
            ListViewChangeItemEvent(originalCardTitleVM,dragCardContent);
        }

        private void CardContentListView_DragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
        {
            if (args.DropResult == Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy) {
                
            } else if (args.DropResult == Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move) {
                //item and item
                //data changes together ,only i need to do is update sql
                //this.model.contents is the updatest

                
            }
        }

        private void AddImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            //animation
            root.Navigate(typeof(AddCardContentPage),this.cardTitleVM, new DrillInNavigationTransitionInfo());
        }

        private void DeleteImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //delete
            DeleteCardListEvent(this.cardTitleVM);
        }

        public CardTitleViewModel cardTitleVM { get; set; }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            //delete
            CardContent clickVM = item.DataContext as CardContent;
            this.cardTitleVM.Contents.Remove(clickVM);
            //remove toast
            if (clickVM.AlarmTime != null) {
                ToastUtil toastUtil = new ToastUtil();
                toastUtil.removeToast(clickVM);
            }
        }

        private void CardContentListView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void CardContentListView_Drop(object sender, DragEventArgs e)
        {
            ListViewChangeItemEvent(this.cardTitleVM, null);
        }


    }

    public class TimeFormatter : IValueConverter
    {
        private void OnScheduleToast(string timeStr,string title,string content)

        {
            ToastUtil toastUtil = new ToastUtil();
            toastUtil.showToast(timeStr,title,content);
        }

        //private string title;
        //private string content;
        // This converts the DateTime object to the string to display.
        public object Convert(object value, Type targetType, 
            object parameter, string language)
        {
            //long timeValue = long.Parse(value.ToString());
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            //DateTime dt = startTime.AddMilliseconds(timeValue);
            //dt.ToString("HH:mm")
            //if (parameter is "title") {
            //    this.title = value as string;
            //    return value;
            //} else if (parameter is "content") {
            //    this.content = value as string;
            //    return value;
            //}
            if (value != null)
            {
                
                string text = "Alarm time：" + value.ToString();
                return text;
            }
            else {
                
                return null;
            }
            
            
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, 
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
