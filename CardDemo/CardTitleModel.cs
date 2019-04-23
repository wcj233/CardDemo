using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDemo
{
    public class CardTitleModel
    {
        private String headerTitle;
        public String HeaderTitle {
            get { return headerTitle; }
            set { headerTitle = value; }
        }

        private ObservableCollection<CardContent> contents;
        public ObservableCollection<CardContent> Contents {
            get { return contents; }
            set { contents = value; }
        }
    }
}
