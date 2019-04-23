using CardDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDemo.VM
{
    public class DisplayCardVM
    {
        private ObservableCollection<Card> mycardcollection;

        public ObservableCollection<Card> MyCardCollection
        {
            get { return mycardcollection; }
            set { mycardcollection = value; }
        }

        public DisplayCardVM()
        {
            initializeData();
        }

        public void initializeData()
        {
            Card card = new Card();
            card.CardTitle = "sss";
            card.CardCt.ContentColor = "blue";
            card.CardCt.ContentDetail = "test1";
            card.CardCt.ContentTitle = "contenttitle";
            mycardcollection.Add(card);
        }
    }
}
