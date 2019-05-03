using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CardDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    //internal delegate void AddContentEventHandler(object parameter);
    public sealed partial class AddCardContentPage : Page
    {
        //internal event AddContentEventHandler AddCardContentEvent;
        private Color mycolor;
        private CardTitleViewModel model;
        private CardContent editCardContent;
        private string selectedAlarmTime;
        public AddCardContentPage()
        {
            this.InitializeComponent();
            mycolor = myColorPicker.Color;
            if (this.editCardContent != null)
            {
                pageTitleTextBlock.Text = "Edit a content";
            }
            else {
                pageTitleTextBlock.Text = "Add a content";
            }
        }

        private void backToPre(object sender, RoutedEventArgs e) {
            Frame.GoBack();

        }

        private void SubmitAction(object sender, RoutedEventArgs e) {
            if (titleTextBox.Text.Length > 0 && contentTextBox.Text.Length > 0)
            {
                if (selectedAlarmTime != null) {
                    DateTime dt = DateTime.Parse(selectedAlarmTime);
                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                    long timeStamp = (long)(dt - startTime).TotalMilliseconds / 1000;
                    DateTime nowDateTime = DateTime.Now;
                    long nowTimeStamp = (long)(nowDateTime - startTime).TotalMilliseconds / 1000;
                    if (timeStamp <= nowTimeStamp)
                    {
                        FlyoutBase.ShowAttachedFlyout((FrameworkElement)alarmTime);
                        return;
                    }
                }
                

                //back
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = mycolor;
                //noti
                string toastId = null;
                if (selectedAlarmTime != null) {
                    ToastUtil toastUtil = new ToastUtil();
                    toastId = toastUtil.showToast(selectedAlarmTime, titleTextBox.Text, contentTextBox.Text);
                }
                if (this.editCardContent != null)
                {
                    if (this.editCardContent.ToastId != null) {
                        //remove old toast
                        ToastUtil toastUtil = new ToastUtil();
                        toastUtil.removeToast(this.editCardContent);
                    }
                    int index = this.model.Contents.IndexOf(this.editCardContent);
                    this.editCardContent = new CardContent { ContentTitle = titleTextBox.Text, ContentDetail = contentTextBox.Text, AlarmTime = selectedAlarmTime, StatusColor = brush, ToastId = toastId }; ;
                    this.model.Contents.RemoveAt(index);
                    this.model.Contents.Insert(index,this.editCardContent);
                }
                else {
                    this.model.Contents.Add(new CardContent { ContentTitle = titleTextBox.Text, ContentDetail = contentTextBox.Text, AlarmTime = selectedAlarmTime, StatusColor = brush, ToastId = toastId });
                }
                
                Frame.GoBack();
            }
            else {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }
        }

        private void MyColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            mycolor = myColorPicker.Color;
            mycolor = Color.FromArgb(myColorPicker.Color.A, myColorPicker.Color.R,myColorPicker.Color.G,myColorPicker.Color.B);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is CardTitleViewModel) {
                this.model = e.Parameter as CardTitleViewModel;
            }
            if (e.Parameter is ObservableCollection<object>) {
                ObservableCollection<object> lists = e.Parameter as ObservableCollection<object>;
                this.model = lists[0] as CardTitleViewModel;
                this.editCardContent = lists[1] as CardContent;
            }
            
        }

        private void AlarmTime_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            //selectedAlarmTime = alarmTime.SelectedTime.TotalMilliseconds;
            //int time = alarmTime.Time.Milliseconds;
            selectedAlarmTime = (sender as TimePicker).Time.ToString(@"hh\:mm");
            DateTime dt = DateTime.Parse(selectedAlarmTime);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(dt - startTime).TotalMilliseconds / 1000;
            DateTime nowDateTime = DateTime.Now;
            long nowTimeStamp = (long)(nowDateTime - startTime).TotalMilliseconds / 1000;
            if (timeStamp <= nowTimeStamp)
            {
                sender.SelectedTime = null;
                selectedAlarmTime = null;
                if (sender.Time.Milliseconds > 0) {
                    FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
                }
            }
                
            

        }
    }

    public class DataConverter : IValueConverter
    {
        private void OnScheduleToast(string timeStr, string title, string content)

        {
            ToastUtil toastUtil = new ToastUtil();
            toastUtil.showToast(timeStr, title, content);
        }

        //private string title;
        //private string content;
        // This converts the DateTime object to the string to display.
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            if (parameter is "ColorPar")
            {
                if (value != null)
                {
                    Brush brush = value as Brush;
                    SolidColorBrush solidBrush = (SolidColorBrush)brush;
                    return solidBrush.Color;
                }
                else {
                    SolidColorBrush solidBrush = new SolidColorBrush();
                    solidBrush.Color = Color.FromArgb(255,255,255,255);
                    return solidBrush.Color;
                }
                
            }
            else {
                if (value != null && value.ToString().Length > 0)
                {
                    TimeSpan duration = TimeSpan.Parse(value.ToString());
                    return duration;
                }
                else
                {
                    TimeSpan s = new TimeSpan();
                    return s;
                }
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
