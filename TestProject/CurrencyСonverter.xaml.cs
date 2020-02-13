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
        private string activeButton;

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

            currency1 = currencyCollection.FirstOrDefault(x => x.CharCode == "USD");
            Currency1CharCode.Text = currency1.CharCode;
            currency2 = currencyCollection.FirstOrDefault(x => x.CharCode == "EUR");
            Currency2CharCode.Text = currency2.CharCode;
        }

        private void ChangeCurrency1_Click(object sender, RoutedEventArgs e)
        {
            activeButton = ChangeCurrency1.Name;
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(SelectCurrency), currencyCollection);
        }

        private void ChangeCurrency2_Click(object sender, RoutedEventArgs e)
        {
            activeButton = ChangeCurrency2.Name;
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(SelectCurrency), currencyCollection);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter != String.Empty) //При первой загрузке e.Parameter равен "" . Не смог иначе обойти данную ошибку.
            {
                Valute selectValute = (Valute)e.Parameter;
                ConvertResult convertResult = new ConvertResult();
                WarningRegex warningRegex = new WarningRegex();
                if (activeButton == ChangeCurrency1.Name)
                {
                    Currency1CharCode.Text = selectValute.CharCode;
                    currency1 = selectValute;
                    if (RegexClass.RegexSum(Currency1.Text))
                        Currency2.Text = convertResult.Result(Currency1.Text, Currency2.Text, currency1, currency2);
                    else
                        Currency1.Text = warningRegex.Warning(Currency1.Text);
                    Currency1.SelectionStart = Currency1.Text.Length;
                }
                if (activeButton == ChangeCurrency2.Name)
                {
                    Currency2CharCode.Text = selectValute.CharCode;
                    currency2 = selectValute;
                    if (RegexClass.RegexSum(Currency2.Text))
                        Currency1.Text = convertResult.Result(Currency2.Text, Currency1.Text, currency2, currency1);
                    else
                        Currency2.Text = warningRegex.Warning(Currency2.Text);
                    Currency2.SelectionStart = Currency2.Text.Length;
                }
            }
        }

        private void Currency1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ConvertResult convertResult = new ConvertResult();
            WarningRegex warningRegex = new WarningRegex();
            if (RegexClass.RegexSum(Currency1.Text))
                Currency2.Text = convertResult.Result(Currency1.Text, Currency2.Text, currency1, currency2);
            else
                Currency1.Text = warningRegex.Warning(Currency1.Text);
            Currency1.SelectionStart = Currency1.Text.Length;
        }

        private void Currency2_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ConvertResult convertResult = new ConvertResult();
            WarningRegex warningRegex = new WarningRegex();
            if (RegexClass.RegexSum(Currency2.Text))
                Currency1.Text = convertResult.Result(Currency2.Text, Currency1.Text, currency2, currency1);
            else
                Currency2.Text = warningRegex.Warning(Currency2.Text);
            Currency2.SelectionStart = Currency2.Text.Length;
        }
    }
}
