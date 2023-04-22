using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FuneralServiceWH_MAUI.Data
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        readonly HttpClient client = new HttpClient();

        public ManufacturerRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Manufacturer>> GetManufacturers()
        {
            var response = await client.GetAsync("api/manufacturers");
            if (response.IsSuccessStatusCode)
            {
                List<Manufacturer> manufacturer = await response.Content.ReadAsAsync<List<Manufacturer>>();
                return manufacturer;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<Manufacturer> GetManufacturer(int ID)
        {
            var response = await client.GetAsync($"api/manufacturers/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Manufacturer manufacturer = await response.Content.ReadAsAsync<Manufacturer>();
                return manufacturer;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddManufacturer(Manufacturer manufacturer)
        {
            var response = await client.PostAsJsonAsync("api/manufacturers", manufacturer);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            var response = await client.PutAsJsonAsync($"api/manufacturers/{manufacturer.Id}", manufacturer);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteManufacturer(Manufacturer manufacturerToDelete)
        {
            var response = await client.DeleteAsync($"api/manufacturers/{manufacturerToDelete.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

    }
}
