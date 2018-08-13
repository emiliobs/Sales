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

        public static string Accept { get => Resource.Accept; }
        public static string Error { get=> Resource.Error; }
        public static string InternetSetting { get=> Resource.InternetSetting; }
        public static string NoInternet { get=>Resource.NoInternet; }
        public static string Products { get=> Resource.Products; }
    }
}
