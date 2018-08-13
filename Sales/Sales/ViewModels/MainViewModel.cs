namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class MainViewModel
    {
        #region Properties

        public ProductsViewModel Products { get; set; }

        #endregion

        #region Contructs

        public MainViewModel()
        {
            Products = new ProductsViewModel();
        }

        #endregion
    }
}
