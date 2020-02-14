using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TestProject.Classes;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace TestProject
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LoadScreen : Page
    {
        private SplashScreen splash;
        internal Frame rootFrame;

        public LoadScreen(SplashScreen splashscreen)
        {
            this.InitializeComponent();
            splash = splashscreen;

            if (splash != null)
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

        }

        void DismissedEventHandler(SplashScreen sender, object e)
        {
            //вызываем метод, в котором парсим данные
            ParserClass.TestParser();
            //по окончанию вызываем метод открытия формы конвертера валют
            DismissExtendedSplash();
        }

        async void DismissExtendedSplash()
        {
            //ждем пока завершится поток и после переходим на новую форму
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                rootFrame = new Frame();
                CurrencyСonverter currencyСonverter = new CurrencyСonverter();
                rootFrame.Content = currencyСonverter;
                Window.Current.Content = rootFrame;
                rootFrame.Navigate(typeof(CurrencyСonverter));
            });
        }
    }
}
