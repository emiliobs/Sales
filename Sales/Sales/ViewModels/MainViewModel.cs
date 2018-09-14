namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Helpers;
    using Sales.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        #endregion

        #region Contructs

        public MainViewModel()
        {
            instance = this;
            //Products = new ProductsViewModel();

            LoadMenu();
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
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginView",
                Title = Languages.Exit,
            });

        }


        private async void GoToAddProduct()
        {
            //instancio la clase justo en el mmento que la necesite:
            AddProduct = new AddProductViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
            //esta propiedad Navigator fuela se creo en la master datail page, para navegar atraves de la masterpage:
            await App.Navigator.PushAsync(new AddProductPage());
        }

        #endregion
    }
}
