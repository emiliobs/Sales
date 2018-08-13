namespace Sales.ViewModels
{
    using Sales.Common.Models;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ProductsViewModel:BaseViewModel
    {
        #region Services
        ApiServices apiService;
        #endregion

        #region Atributtes                     
        ObservableCollection<Product> listProducts;
        #endregion

        #region Properties
        public ObservableCollection<Product> ListProducts
        {
            get => listProducts;
            set
            {
                if (listProducts != value)
                {
                    listProducts = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Contructs
        public ProductsViewModel()
        {
            apiService = new ApiServices();
            LoadProducts();
        }
        #endregion

        #region Mehtods
        private async void LoadProducts()
        {
            //var urlBase = Application.Current.Resources["ApiProduct"].ToString();
            var response = await apiService.GetList<Product>("https://salesapiservices.azurewebsites.net", "/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error.",response.Message,"Accept.");
                return;
            }

            var listProduct = (List<Product>)response.Result;
            //aqui armos las observablecollection a aprtir de una genericcollection(list)
            ListProducts = new ObservableCollection<Product>(listProduct);
        }
        #endregion
    }
}
