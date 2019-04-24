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
        private double selectedAlarmTime;
        public AddCardContentPage()
        {
            this.InitializeComponent();
            mycolor = myColorPicker.Color;
        }

        private void backToPre(object sender, RoutedEventArgs e) {
            Frame.GoBack();

        }

        private void SubmitAction(object sender, RoutedEventArgs e) {
            if (titleTextBox.Text.Length > 0 && contentTextBox.Text.Length > 0)
            {
                //back
                ObservableCollection<CardContent> lists = this.model.Contents;
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = mycolor;
                lists.Add(new CardContent { ContentTitle = titleTextBox.Text,ContentDetail = contentTextBox.Text,AlarmTime = selectedAlarmTime, StatusColor = brush});
                this.model.Contents = lists;
                Frame.GoBack();
            }
            else {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = "Please refine the content";
                ToolTipService.SetToolTip(submitButton, toolTip);
            }
        }

        private void MyColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            mycolor = myColorPicker.Color;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.model = e.Parameter as CardTitleViewModel;
        }

        private void AlarmTime_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            selectedAlarmTime = alarmTime.Time.TotalMilliseconds;
        }
    }
}
