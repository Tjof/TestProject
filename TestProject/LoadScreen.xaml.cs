using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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
    public sealed partial class LoadScreen : Page, INotifyPropertyChanged
    {
        private string url = "https://www.cbr-xml-daily.ru/daily_json.js";
        private ObservableCollection<Valute> currencyCollection;


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        public ObservableCollection<Valute> CurrencyCollection
        {
            get { return currencyCollection; }
            set
            {
                currencyCollection = value;
                OnPropertyChanged();
            }
        }

        public LoadScreen()
        {
            this.InitializeComponent();
            //if (myThread.ThreadState == ThreadState.Stopped)
            //    if (Frame.CanGoForward)
            //        Frame.GoForward();
            //    else
            //        Frame.Navigate(typeof(CurrencyСonverter));

        }

        private void LoadScreen_Load(object sender, EventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(TestParser));
            myThread.Start();
            myThread.Join();
            Frame.Navigate(typeof(CurrencyСonverter));
        }


        public void TestParser()
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string result = streamReader.ReadToEnd();
            RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(result);

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
        }
    }
}
