using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CardDemo
{
    public class CardTitleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private long cardId;
        public long CardId {
            get { return cardId; }
            set
            { cardId = value; }
        }

        private String headerTitle;
        public String HeaderTitle {
            get { return headerTitle; }
            set {
                headerTitle = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CardContent> contents;
        public ObservableCollection<CardContent> Contents {
            get { return contents; }
            set {
                contents = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
