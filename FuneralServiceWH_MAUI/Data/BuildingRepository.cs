using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FuneralServiceWH_MAUI.Data
{
    public class BuildingRepository : IBuildingRepository
    {
        readonly HttpClient client = new HttpClient();

        public BuildingRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Building>> GetBuildings()
        {
            var response = await client.GetAsync("api/Buildings");
            if (response.IsSuccessStatusCode)
            {
                List<Building> buildings = await response.Content.ReadAsAsync<List<Building>>();
                return buildings;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<List<Building>> GetBuilding(int ID)
        {
            var response = await client.GetAsync($"api/Buildings/{ID}");
            if (response.IsSuccessStatusCode)
            {
                List<Building> buildings = await response.Content.ReadAsAsync<List<Building>>();
                return buildings;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddBuilding(Building buildings)
        {
            var response = await client.PostAsJsonAsync("api/Buildings", buildings);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateBuilding(Building buildings)
        {
            var response = await client.PutAsJsonAsync($"api/Buildings/{buildings.Id}", buildings);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteBuilding(Building buildingToDelete)
        {
            var response = await client.DeleteAsync($"api/Buildings/{buildingToDelete.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

    }

}

