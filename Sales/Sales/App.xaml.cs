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
        #region Contructs
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage (new ProductsPage());
            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage (new LoginView());
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
