﻿namespace Sales.Services
{
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Sales.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiServices
    {
        public async Task<Response> CheckConnection()
        {    
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {   
                    IsSuccess = false,    
                    Message = "Please turn on your internet  settting.!", 
                };  
            }  
            var response = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!response)
            {
                return new Response()
                {   
                    IsSuccess = false,
                    Message = "Check your internet connection.!", 
                }; 
            }

            return new Response()
            {
                IsSuccess = true,
                Message = "OK",
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {

                    return new Response()
                    {
                      IsSuccess =false,
                      Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);

                return  new Response()
                {
                   IsSuccess = true,
                   Message = "Ok",
                   Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                   IsSuccess=false,
                   Message = ex.Message,

                };
                
            }
        }
    }
}
