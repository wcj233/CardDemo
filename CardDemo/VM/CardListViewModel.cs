using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace CardDemo
{
    public class CardListViewModel
    {
        private const string cardListsKey = "cardLists";

        private ObservableCollection<CardTitleViewModel> cardLists = new ObservableCollection<CardTitleViewModel>();
        public ObservableCollection<CardTitleViewModel> CardLists {
            get { return this.cardLists; }
            set {
                this.cardLists = value;
            }
        }

        public CardListViewModel() { }

        public CardListViewModel(string jsonString) : this()
        {
            JsonObject jsonObject = JsonObject.Parse(jsonString);
            foreach (IJsonValue jsonValue in jsonObject.GetNamedArray(cardListsKey, new JsonArray()))
            {
                if (jsonValue.ValueType == JsonValueType.Object)
                {
                    cardLists.Add(new CardTitleViewModel(jsonValue.GetObject()));
                }
            }
        }

        public string Stringify()
        {
            JsonArray jsonArray = new JsonArray();
            foreach (CardTitleViewModel cardVM in cardLists)
            {
                jsonArray.Add(cardVM.ToJsonObject());
            }

            JsonObject jsonObject = new JsonObject();
            jsonObject[cardListsKey] = jsonArray;

            return jsonObject.Stringify();
        }

    }
}
