namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        #region Services
        ApiServices apiService;
        #endregion

        #region Atributtes  
        private string filter;
        ObservableCollection<ProductItemViewModel> listProducts;
        bool isRefreshing;
        #endregion

        #region Properties
        public string Filter
        {
            get =>filter;
            set
            {
                if (filter != value)
                {
                    filter = value;
                    this.RefreshList();
                  
                }
            }
        }
        public List<Product> MyProducts { get; set; }

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
        public ObservableCollection<ProductItemViewModel> ListProducts
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
            //singleton
            instance = this;

            //service
            apiService = new ApiServices();

            //Methods
            LoadProducts();
        }
        #endregion

        #region Singlenton

        private static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ProductsViewModel();
            }

            return instance;
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get => new RelayCommand(RefreshList); }

       

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
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                               connection.Message,
                                                               Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await apiService.GetList<Product>(url, urlPrefix, controller);
            // var response = await apiService.GetList<Product>($"https://salesapiservices.azurewebsites.net", 
            //"/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                IsRefreshing = false;
                return;
            }

            MyProducts = (List<Product>)response.Result;

            //Aqui un método para refrezcar la lista:
            this.RefreshList();     

        }

        public void RefreshList()
        {
            //NO esto funciona, pero el lo que no se puede hacer porque es 
            //ineficiente cuando hay 200 o mas registro en la bd:
            //var myList = new List<ProductItemViewModel>();
            //foreach (var item in listProduct)
            //{
            //    myList.Add(new ProductItemViewModel {



            //    });
            // }

            if (string.IsNullOrEmpty(Filter ))
            {
                //Mejor opcion con landa y linq:
                var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
                {
                    Description = p.Description,
                    ImagePath = p.ImagePath,
                    ImageArray = p.ImageArray,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProductId = p.ProductId,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,


                });


                //aqui armos las observablecollection a aprtir de una genericcollection(list)
                ListProducts = new ObservableCollection<ProductItemViewModel>(myListProductItemViewModel.OrderBy(p => p.Description));
                IsRefreshing = false;
            }
            else
            {
                //Mejor opcion con landa y linq:
                var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
                {
                    Description = p.Description,
                    ImagePath = p.ImagePath,
                    ImageArray = p.ImageArray,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProductId = p.ProductId,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,


                }).Where(p=>p.Description.ToLower().Trim().Contains(Filter.ToLower().Trim())).ToList();


            //aqui armos las observablecollection a aprtir de una genericcollection(list)
            ListProducts = new ObservableCollection<ProductItemViewModel>(myListProductItemViewModel.OrderBy(p => p.Description));
            IsRefreshing = false;
            }

           
        }
        #endregion
    }
}
