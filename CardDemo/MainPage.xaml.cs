using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            //this.cardTitleVM = new CardTitleVM();
        }

        private ObservableCollection<CardTitleViewModel> cardLists = new ObservableCollection<CardTitleViewModel>();
        private ObservableCollection<SingleCardUserControl> cardUCLists = new ObservableCollection<SingleCardUserControl>();
        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void AddSuccess_Click(object sender, RoutedEventArgs e)
        {
            //add list view      ***judge is existed the same title***
            tipTextBlock.Text = "";
            if (tipTextBox.Text.Length > 0)
            {
                addFlyout.Hide();
                tipTextBox.Text = "";
                //data
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds;
                ObservableCollection<CardTitleViewModel> lists = new ObservableCollection<CardTitleViewModel>();
                lists.Add(new CardTitleViewModel { CardId = timeStamp, HeaderTitle = "to do", Contents = new ObservableCollection<CardContent>() });
                //add userControl
                SingleCardUserControl singleCardUC = new SingleCardUserControl();
                singleCardUC.cardTitleVM.Contents = new ObservableCollection<CardContent>();
                singleCardUC.cardTitleVM.HeaderTitle = "to do";
                singleCardUC.HorizontalAlignment = HorizontalAlignment.Left;
                singleCardUC.Margin = new Thickness(15, 0, 0, 0);
                singleCardUC.DeleteCardListEvent += deleteCardListAction;
                singleCardUC.ListViewChangeItemEvent += SingleCardUC_ListViewChangeItemEvent;
                CardPanel.Children.Add(singleCardUC);

                tipTextBlock.Text = "";
                this.cardLists = lists;
                this.cardUCLists.Add(singleCardUC);
            }
            else {
                tipTextBlock.Text = "The title is empty.Please enter a title";
            }
            
        }

        private CardContent dragCardContent;
        private CardTitleViewModel originalCardTitleVM;
        private void SingleCardUC_ListViewChangeItemEvent(CardTitleViewModel exchangeCardTitleVM, CardContent exchangeContent)
        {
            if (exchangeContent != null)
            {
                this.dragCardContent = exchangeContent;
                this.originalCardTitleVM = exchangeCardTitleVM;
            }
            else {
                //complete
                this.originalCardTitleVM.Contents.Remove(this.dragCardContent);
                exchangeCardTitleVM.Contents.Insert(0,this.dragCardContent);
            }
        }



        //public CardTitleVM cardTitleVM { get; set; }

        private async void deleteCardListAction(object parameter) {
            //dialog
            CardTitleViewModel model = parameter as CardTitleViewModel;
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete",
                Content = "Are you sure to delete" + " \"" + model.HeaderTitle + " \" list",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                int index = 0;
                for (int i = 0; i < this.cardLists.Count; i++)
                {
                    CardTitleViewModel singleVM = this.cardLists[i];
                    if (singleVM.CardId == model.CardId)
                    {
                        index = i;
                        break;
                    }
                }
                SingleCardUserControl deleteCardUC = this.cardUCLists[index];
                CardPanel.Children.Remove(deleteCardUC);
                this.cardUCLists.RemoveAt(index);
                this.cardLists.RemoveAt(index);

                foreach (CardContent content in deleteCardUC.cardTitleVM.Contents) {
                    if (content.AlarmTime != null) {
                        ToastUtil toastUtil = new ToastUtil();
                        toastUtil.removeToast(content);
                    }
                }

            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CardPanel_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void CardPanel_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
