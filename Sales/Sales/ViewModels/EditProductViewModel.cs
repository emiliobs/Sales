namespace Sales.ViewModels
{
    using Plugin.Media.Abstractions;
    using Sales.Common.Models;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;

    public class EditProductViewModel: BaseViewModel
    {

        #region Services

        private ApiServices ApiServices;

        #endregion

        #region Attributtes
        private Product product;

        //aqui queda almacenada la foto con el plugin de media:
        private MediaFile file;
        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;

        #endregion

        #region Properties

        public bool IsRunning
        {
            get => isRunning;
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
        public ImageSource ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public Product Product
        {
            get => product;
            set
            {
                if (product != value)
                {
                    product = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constrictor
        public EditProductViewModel(Product product)
        {
            this.product = product;

            ApiServices = new ApiServices();

            IsEnabled = true;
            ImageSource = product.ImageFullPath;
        }



        #endregion
    }
}
