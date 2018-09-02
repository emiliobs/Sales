namespace Sales.Helpers
{
    using Sales.Interfaces;
    using Sales.Resources;
    using Xamarin.Forms;

    public class Languages
    {
        public Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string EditProducts { get => Resource.EditProducts; }
        public static string IsAvailable { get => Resource.IsAvailable; }
        public static string Confirm { get => Resource.Confirm; }
        public static string Delete { get => Resource.Delete; }
        public static string DeleteConfirmation { get => Resource.DeleteConfirmation; }
        public static string Yes { get => Resource.Yes; }
        public static string No { get => Resource.No; }
        public static string Edit { get => Resource.Edit; }
        public static string Cancel { get => Resource.Cancel; }
        public static string NewPicture { get => Resource.NewPicture; }
        public static string FromGallery { get => Resource.FromGallery; }
        public static string ImageSource { get => Resource.ImageSource; }
        public static string DescriptionError { get => Resource.DescriptionError; }
        public static string PriceError { get => Resource.PriceError; }
        public static string Accept { get => Resource.Accept; }
        public static string ChangeImage { get => Resource.ChangeImage; }
        public static string Error { get=> Resource.Error; }
        public static string InternetSetting { get=> Resource.InternetSetting; }
        public static string NoInternet { get=>Resource.NoInternet; }
        public static string Products { get=> Resource.Products; }
        public static string AddProducts { get => Resource.AddProducts; }
        public static string AddProductsToolBar { get => Resource.AddProductsToolBar; }
        public static string Description { get => Resource.Description; }
        public static string DescriptionPlaceHolder { get => Resource.DescriptionPlaceHolder; }
        public static string Price { get => Resource.Price; }
        public static string PricePlaceHolder { get => Resource.PricePlaceHolder; }
        public static string Remarks { get => Resource.Remarks; }
        public static string Save { get => Resource.Save; }
    }
}
