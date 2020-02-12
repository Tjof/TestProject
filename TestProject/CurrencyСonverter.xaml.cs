using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using TestProject.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace TestProject
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CurrencyСonverter : Page
    {
        private string url = "https://www.cbr-xml-daily.ru/daily_json.js";
        Valute currency1;
        Valute currency2;
        ObservableCollection<Valute> currencyCollection = new ObservableCollection<Valute>();

        public CurrencyСonverter()
        {
            this.InitializeComponent();
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string result = streamReader.ReadToEnd();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(result);

            ChangeCurrency1.Content = string.Format("Изменить\nвалюту");
            ChangeCurrency2.Content = string.Format("Изменить\nвалюту");

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

            currency1 = currencyCollection.FirstOrDefault(x=>x.CharCode == "USD");
            Currency1CharCode.Text = currency1.CharCode;
            currency2 = currencyCollection.FirstOrDefault(x=>x.CharCode == "EUR");
            Currency2CharCode.Text = currency2.CharCode;
        }

        private void ChangeCurrency1_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(SelectCurrency), currencyCollection);
        }

        private void ChangeCurrency2_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(SelectCurrency), currencyCollection);
        }

        private void Currency1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int currencySum = Int32.Parse(Currency1.Text);
            float result = (currency1.Value / currency1.Nominal * currencySum) / (currency2.Value / currency2.Nominal);
            Currency2.Text = result.ToString();
        }

        private void Currency2_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
