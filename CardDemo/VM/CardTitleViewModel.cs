using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace CardDemo
{
    public class CardTitleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private const string cardIdKey = "cardId";
        private const string headerTitleKey = "headerTitle";
        private const string contentsKey = "contents";
        private const string cardVMKey = "cardVM";

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

        private ObservableCollection<CardContent> contents = new ObservableCollection<CardContent>();
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

        public CardTitleViewModel() {

        }



        //json
        public CardTitleViewModel(JsonObject jsonObject)
        {
            //JsonObject jsonObject = JsonObject.Parse(jsonString);
            //cardId = Convert.ToInt64(jsonObject.GetNamedString(cardIdKey, ""));

            //IJsonValue phoneJsonValue = jsonObject.GetNamedValue(headerTitleKey);
            //headerTitle = phoneJsonValue.GetString();
            //foreach (IJsonValue jsonValue in jsonObject.GetNamedArray(contentsKey, new JsonArray()))
            //{
            //    if (jsonValue.ValueType == JsonValueType.Object)
            //    {
            //        contents.Add(new CardContent(jsonValue.GetObject()));
            //    }
            //}

            JsonObject cardObject = jsonObject.GetNamedObject(cardVMKey, null);
            cardId = Convert.ToInt64(cardObject.GetNamedString(cardIdKey, ""));
            headerTitle = cardObject.GetNamedString(headerTitleKey, "");
            foreach (IJsonValue jsonValue in cardObject.GetNamedArray(contentsKey, new JsonArray()))
            {
                if (jsonValue.ValueType == JsonValueType.Object)
                {
                    contents.Add(new CardContent(jsonValue.GetObject()));
                }
            }
        }

        public string Stringify()
        {
            JsonArray jsonArray = new JsonArray();
            foreach (CardContent card in contents)
            {
                jsonArray.Add(card.ToJsonObject());
            }

            JsonObject jsonObject = new JsonObject();
            jsonObject[cardIdKey] = JsonValue.CreateStringValue(cardId.ToString());

            // Treating a blank string as null
            jsonObject[headerTitleKey] = JsonValue.CreateStringValue(headerTitle);
            jsonObject[contentsKey] = jsonArray;
            return jsonArray.Stringify();
            //return jsonObject.Stringify();
        }

        

        public JsonObject ToJsonObject()
        {
            //JsonObject cardObject = new JsonObject();
            //cardObject.SetNamedValue(cardIdKey, JsonValue.CreateStringValue(cardId.ToString()));
            //cardObject.SetNamedValue(headerTitleKey, JsonValue.CreateStringValue(headerTitle));

            //cardObject.SetNamedValue(contentsKey, JsonValue.CreateStringValue(this.Stringify()));

            //JsonObject jsonObject = new JsonObject();
            //jsonObject.SetNamedValue(cardVMKey, cardObject);


            //return jsonObject;

            JsonArray jsonArray = new JsonArray();
            foreach (CardContent card in contents)
            {
                jsonArray.Add(card.ToJsonObject());
            }

            JsonObject cardObject = new JsonObject();
            cardObject[cardIdKey] = JsonValue.CreateStringValue(cardId.ToString());

            // Treating a blank string as null
            cardObject[headerTitleKey] = JsonValue.CreateStringValue(headerTitle);
            cardObject[contentsKey] = jsonArray;

            JsonObject jsonObject = new JsonObject();
            jsonObject.SetNamedValue(cardVMKey, cardObject);


            return jsonObject;

        }



    }
}
