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
        private string contentTitle;
        public string ContentTitle {
            get { return contentTitle; }
            set { contentTitle = value; }
        }

        private string contentDetail;
        public string ContentDetail
        {
            get { return contentDetail; }
            set { contentDetail = value; }
        }
        private Color statusColor;

        public Color StatusColor {
            get { return statusColor; }
            set { statusColor = value; }
        }
    }

}
