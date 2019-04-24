using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

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

        private string alarmTime;
        public string AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        private Brush statusColor;

        public Brush StatusColor {
            get { return statusColor; }
            set { statusColor = value; }
        }
    }

}
