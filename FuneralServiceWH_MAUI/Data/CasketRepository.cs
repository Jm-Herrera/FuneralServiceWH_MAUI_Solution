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
    public class CasketRepository : ICasketRepository
    {
        readonly HttpClient client = new HttpClient();

        public CasketRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Casket>> GetCaskets()
        {
            HttpResponseMessage response = await client.GetAsync("api/caskets");
            if (response.IsSuccessStatusCode)
            {
                List<Casket> caskets = await response.Content.ReadAsAsync<List<Casket>>();
                return caskets;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<List<Casket>> GetCasketsByBuilding(int BuildingID)
        {
            var response = await client.GetAsync($"api/caskets/byBuilding/{BuildingID}");
            if (response.IsSuccessStatusCode)
            {
                List<Casket> caskets = await response.Content.ReadAsAsync<List<Casket>>();
                return caskets;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Casket> GetCasket(int ID)
        {
            var response = await client.GetAsync($"api/caskets/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Casket caskets = await response.Content.ReadAsAsync<Casket>();
                return caskets;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddCasket(Casket casketToAdd)
        {
            casketToAdd.Building = null;
            var response = await client.PostAsJsonAsync("api/caskets", casketToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateCasket(Casket casketToUpdate)
        {
            casketToUpdate.Building = null;
            var response = await client.PutAsJsonAsync($"api/caskets/{casketToUpdate.Id}", casketToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteCasket(Casket casketToDelete)
        {
            var response = await client.DeleteAsync($"api/caskets/{casketToDelete.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

    }
}

