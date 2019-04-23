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

    public class CardTitleModel : INotifyPropertyChanged
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
        //private ObservableCollection<CardContent> myexistContents = new ObservableCollection<CardContent>();
        //public ObservableCollection<CardContent> existContents
        //{
        //    get { return this.myexistContents; }
        //    set
        //    {
        //        this.myexistContents = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        public CardTitleModel() {
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    } 
}
