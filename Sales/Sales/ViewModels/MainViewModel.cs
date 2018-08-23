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

        #endregion

        #region Contructs

        public MainViewModel()
        {
            Products = new ProductsViewModel();
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
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }

        #endregion
    }
}
