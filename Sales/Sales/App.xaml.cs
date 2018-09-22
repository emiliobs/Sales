using Newtonsoft.Json;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.ViewModels;
using Sales.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Sales
{
	public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigator { get; internal set; } 
        #endregion

        #region Contructs
        public App()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();

            if (Settings.IsRemembered)
            {

                if (!string.IsNullOrEmpty(Settings.UserASP))
                {
                    mainViewModel.UserASP = JsonConvert.DeserializeObject<MyUserASP>(Settings.UserASP);
                }

                mainViewModel.Products = new ProductsViewModel();
                this.MainPage = new MasterPage();
            }
            else
            {
                mainViewModel.Login = new LoginViewModel();
                
                this.MainPage = new NavigationPage(new LoginView());
            }


            //if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.AccessToken))
            //{
            //    MainViewModel.GetInstance().Products = new ProductsViewModel();
            //    //la mamster page tiene su propio navegador
            //    MainPage = new MasterPage();
            //    //MainPage = new NavigationPage(new ProductsPage());
            //}
            //else
            //{
            //    // MainPage = new NavigationPage (new ProductsPage());
            //    MainViewModel.GetInstance().Login = new LoginViewModel();
            //    MainPage = new NavigationPage(new LoginView());
            //}


        }

        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
