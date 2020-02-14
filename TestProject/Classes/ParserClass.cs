using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Classes
{
    class ParserClass
    {
        private static string url = "https://www.cbr-xml-daily.ru/daily_json.js";
        private static ObservableCollection<Valute> currencyCollection = new ObservableCollection<Valute>();
        public static void TestParser()
        {
            //создаем парсер и читаем в строку
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string result = streamReader.ReadToEnd();
            //разбиваем строку по классам
            QuoteData rootobject = JsonConvert.DeserializeObject<QuoteData>(result);

            //заполняем коллекцию полученными данными
            foreach (var ret in rootobject.Valute)
            {
                var item = ret.Value;
                var valute = new Valute
                {
                    ID = item.ID,
                    NumCode = item.NumCode,
                    CharCode = item.CharCode,
                    Nominal = item.Nominal,
                    Name = item.Name,
                    Value = item.Value,
                    Previous = item.Previous
                };
                currencyCollection.Add(valute);
            }
            Variables.currencyCollection = currencyCollection;
        }
    }
}
