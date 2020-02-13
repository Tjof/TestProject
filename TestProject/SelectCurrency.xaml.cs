using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TestProject.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace TestProject
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SelectCurrency : Page, INotifyPropertyChanged
    {
        ObservableCollection<Valute> currencyCollection = new ObservableCollection<Valute>();
        private ObservableCollection<Valute> resultList;

        public SelectCurrency()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<Valute> ResultList
        {
            get
            {
                return resultList;
            }
            set
            {
                resultList = value;
                OnPropertyChanged();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                currencyCollection = (ObservableCollection<Valute>)e.Parameter;
                ResultList = currencyCollection;
                ValuteList.ItemsSource = ResultList;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        private void valuteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectValute = ValuteList.SelectedItem as Valute;
            if (Frame.CanGoForward)
                Frame.GoForward();
            else
                Frame.Navigate(typeof(CurrencyСonverter), selectValute);
        }
    }
}
