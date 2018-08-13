namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductsViewModel:BaseViewModel
    {
        #region Services
        ApiServices apiService;
        #endregion

        #region Atributtes                     
        ObservableCollection<Product> listProducts;
        bool isRefreshing;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }
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

        #region Commands
        public ICommand RefreshCommand { get => new RelayCommand(Refresh); }

        #endregion

        #region Mehtods

        private void Refresh()
        {
            LoadProducts();
        }
        private async void LoadProducts()
        {
            IsRefreshing = true;

            //Aqui valido si hay conecction con  internet:
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error.",connection.Message,"Accept.");
                return;
            }

            //var urlBase = Application.Current.Resources["ApiProduct"].ToString();
            var response = await apiService.GetList<Product>("https://salesapiservices.azurewebsites.net", "/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error.",response.Message,"Accept.");
                IsRefreshing = false;
                return;
            }

            var listProduct = (List<Product>)response.Result;
            //aqui armos las observablecollection a aprtir de una genericcollection(list)
            ListProducts = new ObservableCollection<Product>(listProduct);
            IsRefreshing = false;
        }
        #endregion
    }
}
