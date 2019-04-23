using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CardDemo
{
    public class CardContent {
        public string contentTitle = "today";
        public string contentDetail = "I want to review";
        public Color statusColor;
    }
    public class CardContentModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CardContent> cardContents = new ObservableCollection<CardContent>();
        public ObservableCollection<CardContent> CardContents {
            get { return cardContents; }
            set
            {
                cardContents = value;
                OnPropertyChanged();
            }
        }

        public CardContentModel() {
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
