using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDemo.Model
{
    public class Card
    {
        private CardContent cardct;

        public CardContent CardCt
        {
            get { return cardct; }
            set { cardct = value; }
        }

        private string cardtitle;

        public string CardTitle
        {
            get { return cardtitle; }
            set { cardtitle = value; }
        }

    }
}
