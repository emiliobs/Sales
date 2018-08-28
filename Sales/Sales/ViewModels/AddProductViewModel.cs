namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddProductViewModel:BaseViewModel
    {
        #region Services

        private ApiServices ApiServices;

        #endregion

        #region Atributtes
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties
        public string Description { get; set; }
        public string Price { get; set; }
        public string Remarks { get; set; }
        public bool IsRunning
        { get => isRunning;
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged();
                }
            }

        }
        #endregion

        #region Constructor
        public AddProductViewModel()
        {
            ApiServices = new ApiServices();

            IsEnabled = true;
          
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get => new RelayCommand(Save); }

        #endregion

        #region Methods

        private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, 
                    Languages.DescriptionError, 
                    Languages.Accept);
                
                return;
            }

            if (string.IsNullOrEmpty(Price))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PriceError, Languages.Accept);

                return;
            }

            var price = decimal.Parse(Price);

            if (price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                    Languages.PriceError, 
                    Languages.Accept);
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            //Aqui valido si hay conecction con  internet:
            var connection = await ApiServices.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, 
                                                                connection.Message,
                                                                Languages.Accept);
                return;
            }

            //aqui armo el objeto con las variables desde el formulario add new product:
            var product = new Product
            {
               Description = Description,
               Price       = price,
               Remarks     = Remarks,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await ApiServices.Post(url, urlPrefix, controller, product);


            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                              response.Message,
                                                              Languages.Accept);
                return;

            }

            //new product
            var newProduct = (Product)response.Result;
            //patron sigleton
            var productViewModel = ProductsViewModel.GetInstance();
            productViewModel.ListProducts.Add(newProduct);
           
            IsRunning = false;
            IsEnabled = true;

            //aqui desapilo y regreso a la página anterior:
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}
