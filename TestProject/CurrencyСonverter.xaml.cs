using System.Linq;
using TestProject.Classes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace TestProject
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CurrencyСonverter : Page
    {
        public CurrencyСonverter()
        {
            this.InitializeComponent();

            //первая валюта по стандарту USD
            Variables.currency1 = Variables.currencyCollection.FirstOrDefault(x => x.CharCode == "USD");
            Currency1CharCode.Text = Variables.currency1.CharCode;
            //вторая валюта по стандарту EUR
            Variables.currency2 = Variables.currencyCollection.FirstOrDefault(x => x.CharCode == "EUR");
            Currency2CharCode.Text = Variables.currency2.CharCode;
        }

        private void ChangeCurrency1_Click(object sender, RoutedEventArgs e)
        {
            //устанавливаем значение активной кнопки, чтобы запомнить, какую валюту следует изменить
            Variables.activeButton = ChangeCurrency1.Name;
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(SelectCurrency), Variables.currencyCollection);
        }

        private void ChangeCurrency2_Click(object sender, RoutedEventArgs e)
        {
            Variables.activeButton = ChangeCurrency2.Name;
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(SelectCurrency), Variables.currencyCollection);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Valute selectValute = (Valute)e.Parameter;
            ConvertResult convertResult = new ConvertResult();
            //если значение активной кнопки равно первой валюте - изменяем значение второй валюты 
            if (Variables.activeButton == ChangeCurrency1.Name)
            {
                Currency1CharCode.Text = selectValute.CharCode;
                Variables.currency1 = selectValute;
                Currency2.Text = convertResult.Result(Currency1.Text, Currency2.Text, Variables.currency1, Variables.currency2);
                Currency1.SelectionStart = Currency1.Text.Length;
            }
            if (Variables.activeButton == ChangeCurrency2.Name)
            {
                Currency2CharCode.Text = selectValute.CharCode;
                Variables.currency2 = selectValute;Currency1.Text = convertResult.Result(Currency2.Text, Currency1.Text, Variables.currency2, Variables.currency1);
                Currency2.SelectionStart = Currency2.Text.Length;
            }
        }

        private void Currency1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ConvertResult convertResult = new ConvertResult();
            WarningRegex warningRegex = new WarningRegex();
            //проверяем регуляркой введенное значение в TextBox, если проходит - изменяем значение второй валюты
            if (RegexClass.RegexSum(Currency1.Text))
                Currency2.Text = convertResult.Result(Currency1.Text, Currency2.Text, Variables.currency1, Variables.currency2);
            //если не проходит - удаляем последний введеный символ
            else
                Currency1.Text = warningRegex.Warning(Currency1.Text);
            //курсор в конец строки TextBox'a
            Currency1.SelectionStart = Currency1.Text.Length;
        }

        private void Currency2_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ConvertResult convertResult = new ConvertResult();
            WarningRegex warningRegex = new WarningRegex();
            if (RegexClass.RegexSum(Currency2.Text))
                Currency1.Text = convertResult.Result(Currency2.Text, Currency1.Text, Variables.currency2, Variables.currency1);
            else
                Currency2.Text = warningRegex.Warning(Currency2.Text);
            Currency2.SelectionStart = Currency2.Text.Length;
        }
    }
}
