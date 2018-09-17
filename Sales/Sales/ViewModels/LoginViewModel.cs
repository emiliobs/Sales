﻿namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Helpers;
    using Sales.Services;
    using Sales.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel:BaseViewModel
    {
        #region Services
        private ApiServices apiService;
        #endregion

        #region Atributtes       
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
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


        #endregion

        #region Contructors
        public LoginViewModel()
        {
            //Service
            apiService = new ApiServices();

            //Properties
            IsEnabled = true;
            IsRemembered = true;

            Email = "barrera_emilio@hotmail.com";
            Password = "Eabs123.";


        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get => new RelayCommand(Login); }

        #endregion

        #region Methods 
        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept
                    );

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept
                    );

                return;
            }

            IsRunning = true;
            IsEnabled = false;
            //Aqui valido si hay conecction con  internet:
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                               connection.Message,
                                                               Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var token = await apiService.GetToken(url,Email,Password);

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error,
                                                               Languages.SomethingWrong,
                                                               Languages.Accept);
                return;
            }

            //aqui guado en persistncia los token que viene del servicio:(aqui  lo guarada el plugin en disco)
            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.IsRemembered = IsRemembered;


            //Aqui instacion la page con la pagina sin navegacion de back productpage();
            MainViewModel.GetInstance().Products = new ProductsViewModel();
            // Application.Current.MainPage = new ProductsPage();
            Application.Current.MainPage = new MasterPage();
            //App.Current.MainPage = new MasterPage();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
