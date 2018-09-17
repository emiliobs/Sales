namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Sales.Helpers;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RegisterPageViewModel:BaseViewModel
    {

        #region Services

        #endregion
        #region Attributtes
        private MediaFile file;
        private ImageSource imageSource;
        private ApiServices apiServices;
        private bool isRunning;
        private bool isenabled;
        #endregion

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get => isRunning;
            set
            {
                if (isRunning != value )
                {
                    isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => isenabled;
            set
            {
                if (isenabled != value)
                {
                    isenabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource ImageSource
        {
            get =>imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Constructor
        public RegisterPageViewModel()
        {
            apiServices = new ApiServices();

            ImageSource = "nouser";
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand { get => new RelayCommand(ChangeImage); }


        #endregion

        #region Methods
        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();
            var souce = await Application.Current.MainPage.DisplayActionSheet(
                 Languages.ImageSource,
                 Languages.Cancel, null,
                 Languages.FromGallery,
                 Languages.NewPicture
                );

            if (souce == Languages.Cancel)
            {
                file = null;
                return;
            }

            if (souce == Languages.NewPicture)
            {
                file = await CrossMedia.Current.TakePhotoAsync(

                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test,jpg",
                        PhotoSize = PhotoSize.Small,
                    }

                    );
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }
        #endregion
    }
}
