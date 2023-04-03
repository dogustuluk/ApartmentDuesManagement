using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Web.Models.Apartments;

namespace Web.Services
{
    public class ApartmentsApiService
    {
        private readonly HttpClient _httpClient;

        public ApartmentsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }
        public async Task<List<Apartment>> GetApartmentsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Apartment>>("/api/Apartment/getapartmentlist");
            return response.ToList();
        }
    }
}
