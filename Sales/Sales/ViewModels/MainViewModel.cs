namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties

        public ProductsViewModel Products { get; set; }
        public AddProductViewModel AddProduct { get; set; }
        public EditProductViewModel EditProduct { get; set; }
        public LoginViewModel Login { get; set; }

        #endregion

        #region Contructs

        public MainViewModel()
        {
            instance = this;
            //Products = new ProductsViewModel();
        }

        #endregion

          #region Singlenton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        #endregion

        #region Commands

        public ICommand AddProductCommand { get => new RelayCommand(GoToAddProduct); }



        #endregion

        #region Methods

        private async void GoToAddProduct()
        {
            //instancio la clase justo en el mmento que la necesite:
            AddProduct = new AddProductViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
            await App.Navigator.PushAsync(new AddProductPage());
        }

        #endregion
    }
}
