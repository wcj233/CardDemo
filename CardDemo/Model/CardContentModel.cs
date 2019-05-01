using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CardDemo
{
    public class CardContent {

        private const string toastIdKey = "toastId";
        private const string contentTitleKey = "contentTitle";
        private const string contentDetailKey = "contentDetail";
        private const string alarmTimeKey = "alarmTime";
        private const string statusColorKey = "statusColor";
        private const string cardContentKey = "cardContent";


        private string toastId;
        public string ToastId
        {
            get { return toastId; }
            set { toastId = value; }
        }

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

        public CardContent() {

        }

        public CardContent(JsonObject jsonObject)
        {
            JsonObject cardObject = jsonObject.GetNamedObject(cardContentKey, null);
            toastId = cardObject.GetNamedString(toastIdKey, "");
            contentTitle = cardObject.GetNamedString(contentTitleKey, "");
            contentDetail = cardObject.GetNamedString(contentDetailKey, "");
            alarmTime = cardObject.GetNamedString(alarmTimeKey,"");
            string c = cardObject.GetNamedString(statusColorKey);
            string[] colorArray = cardObject.GetNamedString(statusColorKey).Split(',');
            SolidColorBrush brush = new SolidColorBrush();
            byte b = Convert.ToByte(colorArray[0]);
            brush.Color = Color.FromArgb(Convert.ToByte(colorArray[0]), Convert.ToByte(colorArray[1]), Convert.ToByte(colorArray[2]), Convert.ToByte(colorArray[3]));
            statusColor = brush;
        }

        public JsonObject ToJsonObject()
        {
            JsonObject cardObject = new JsonObject();
            if (toastId == null)
            {
                cardObject.SetNamedValue(toastIdKey, JsonValue.CreateStringValue(""));
            }
            else {
                cardObject.SetNamedValue(toastIdKey, JsonValue.CreateStringValue(toastId));
            }
            
            cardObject.SetNamedValue(contentTitleKey, JsonValue.CreateStringValue(contentTitle));
            cardObject.SetNamedValue(contentDetailKey, JsonValue.CreateStringValue(contentDetail));
            if (alarmTime == null)
            {
                cardObject.SetNamedValue(alarmTimeKey, JsonValue.CreateStringValue(""));
            }
            else
            {
                cardObject.SetNamedValue(alarmTimeKey, JsonValue.CreateStringValue(alarmTime));
            }
            
            SolidColorBrush colorBrush = (SolidColorBrush)statusColor;
            string colorStr = colorBrush.Color.A + "," + colorBrush.Color.B + "," + colorBrush.Color.G + "," + colorBrush.Color.R;
            cardObject.SetNamedValue(statusColorKey, JsonValue.CreateStringValue(colorStr));

            JsonObject jsonObject = new JsonObject();
            jsonObject.SetNamedValue(cardContentKey, cardObject);
            

            return jsonObject;
        }
    }

}
