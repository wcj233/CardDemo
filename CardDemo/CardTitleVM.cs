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
    public class CardTitle {
        public String cardTitle = "to do";
        public ObservableCollection<CardContent> contents = new ObservableCollection<CardContent>();
    }

    public class CardTitleVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ObservableCollection<CardTitle> myCardTitles = new ObservableCollection<CardTitle>();
        public ObservableCollection<CardTitle> CardTitles {
            get { return this.myCardTitles; }
            set
            {
                this.myCardTitles = value;
                this.OnPropertyChanged();
            }
        }

        public CardTitleVM() {
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    } 
}
