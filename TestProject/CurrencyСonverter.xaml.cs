using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public CurrencyСonverter()
        {
            this.InitializeComponent();
            //создаем запрос к сайту по заданному URL
            WebRequest webRequest = WebRequest.Create("https://www.cbr-xml-daily.ru/daily_json.js");
            //если бы нужно было настроить параметры, это было бы здесь
            //получаем ответ
            WebResponse webResponse = webRequest.GetResponse();
            //создаем поток данных для чтения ответа
            Stream stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            //читаем html страничку в строку
            string result = streamReader.ReadToEnd();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(result);

            ChangeCurrency1.Content = string.Format("Изменить\nвалюту");
            ChangeCurrency2.Content = string.Format("Изменить\nвалюту");
        }
    }
}
