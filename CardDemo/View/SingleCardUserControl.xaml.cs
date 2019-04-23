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
        }

        private void AddImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            //这里参数自动装箱
            root.Navigate(typeof(AddCardContentPage));
        }

        private void DeleteImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //delete
            DeleteCardListEvent(this.cardTitleModel);
        }

        public CardTitleViewModel cardTitleModel { get; set; }
    }
}
