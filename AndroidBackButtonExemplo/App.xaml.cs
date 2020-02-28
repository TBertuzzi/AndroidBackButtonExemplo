using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidBackButtonExemplo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public bool ConfirmaFecharApp
        {
            get
            {
                bool ultimaView = false;
                switch (MainPage)
                {
                    // Unica Pagina
                    case ContentPage _:
                        ultimaView = true;
                        break;
                    // É NavigationPage ou TabbedPage
                    case NavigationPage mainPage when mainPage.CurrentPage is TabbedPage tabbedPage
                                                      && tabbedPage.CurrentPage is NavigationPage navigationPage:
                        ultimaView = navigationPage.Navigation.NavigationStack.Count <= 1;
                        break;
                    // A NavigationPage é a Pagina Principal
                    case NavigationPage mainPage:
                        ultimaView = mainPage.Navigation.NavigationStack.Count <= 1;
                        break;
                    // A TabbedPage é a Pagina Principal
                    case TabbedPage tabbedPage when tabbedPage.CurrentPage is NavigationPage navigationPage:
                        ultimaView = navigationPage.Navigation.NavigationStack.Count <= 1;
                        break;
                }
                return ultimaView;
            }
        }
    }
}
