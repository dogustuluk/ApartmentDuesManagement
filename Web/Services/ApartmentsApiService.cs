using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web.Models.Apartments;

namespace Web.Services
{
    public class ApartmentsApiService
    {
        private readonly HttpClient _httpClient;

        public ApartmentsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Apartment>> GetApartmentsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<SuccessDataResult<List<Apartment>>>("Apartment/getapartmentlist");
            //var response =  _httpClient.GetAsync("Apartment/getapartmentlist").Result;

            return response.Data;


        }
    }
}
