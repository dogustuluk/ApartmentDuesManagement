
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.EntityFramework.Context;
using Entities.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<List<ApartmentDto>> GetApartmentsAsync()
        {

            //var response = await _httpClient.GetFromJsonAsync<List<ApartmentDto>>("Apartment/getapartmentlist");
            //return response;
            //var response = await _httpClient.GetFromJsonAsync<List<ApartmentDto>>("Apartment/getapartmentlist");

            //var res = await _httpClient.GetAsync("Apartment/getapartmentlist");

            //string responseContent = await res.Content.ReadAsStringAsync();
            //var datas = JsonConvert.DeserializeObject<List<ApartmentDto>>(responseContent);

            //return datas.ToList();
            // --
            ///
            #region 1.yol
            var response = await _httpClient.GetAsync("Apartment/getapartmentlist");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            var apartmentList = new List<ApartmentDto>();
            var values = jsonObject["data"]["$values"];
            foreach (var value in values)
            {
                var apartment = value.ToObject<ApartmentDto>();
                apartmentList.Add(apartment);
            }

            return apartmentList;
            #endregion


            /*2.yol
             * task.whenall ile tum gorevlerin tamamlanmasi gerekir.
             * bu sekilde birden fazla istegi paralel hale getirip sorgunun performansi artar ancak sunucunun da bu paralel istekleri kabul etmesi lazim.
             */
            //var response = await _httpClient.GetAsync("Apartment/getapartmentlist");
            //response.EnsureSuccessStatusCode();

            //var jsonString = await response.Content.ReadAsStringAsync();
            //var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            //var apartmentList = new List<Apartment>();
            //var values = jsonObject["data"]["$values"];

            //var tasks = new List<Task<Apartment>>();
            //foreach (var value in values)
            //{
            //    var task = Task.Run(() =>
            //    {
            //        var apartment = value.ToObject<Apartment>();
            //        return apartment;
            //    });
            //    tasks.Add(task);
            //}

            //await Task.WhenAll(tasks);

            //foreach (var task in tasks)
            //{
            //    apartmentList.Add(task.Result);
            //}

            //return apartmentList;


            //try
            //{
            //    var response = await _httpClient.GetFromJsonAsync<DataResult<List<Apartment>>>("Apartment/getapartmentlist");

            //    if (response.Success)
            //    {
            //        return new SuccessDataResult<List<Apartment>>(response.Data, response.Message);
            //    }
            //    else
            //    {
            //        return new ErrorDataResult<List<Apartment>>(response.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle any exceptions here
            //    return new ErrorDataResult<List<Apartment>>(ex.Message);
            //}

            //--
            //var response = await _httpClient.GetAsync("http://localhost:5273/api/Apartment/getapartmentlist");
            //if (response.IsSuccessStatusCode)
            //{
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    var result = JsonConvert.DeserializeObject<DataResult<List<Apartment>>>(responseContent);
            //    if (result.Success)
            //    {
            //        return new SuccessDataResult<List<Apartment>>(result.Data, result.Message);
            //    }
            //    else
            //    {
            //        return new ErrorDataResult<List<Apartment>>(result.Message);
            //    }
            //}
            //else
            //{
            //    return new ErrorDataResult<List<Apartment>>($"Request failed with status code {response.StatusCode}");
            //}
        }

    }
    
}
