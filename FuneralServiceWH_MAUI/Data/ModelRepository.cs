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
    public class ModelRepository : IModelRepository
    {

        readonly HttpClient client = new HttpClient();

        public ModelRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Model>> GetModels()
        {
            var response = await client.GetAsync("api/models");
            if (response.IsSuccessStatusCode)
            {
                List<Model> model = await response.Content.ReadAsAsync<List<Model>>();
                return model;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<Model> GetModel(int ID)
        {
            var response = await client.GetAsync($"api/models/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Model model = await response.Content.ReadAsAsync<Model>();
                return model;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddModel(Model models)
        {
            var response = await client.PostAsJsonAsync("api/models", models);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateModel(Model models)
        {
            var response = await client.PutAsJsonAsync($"api/models/{models.Id}", models);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteModel(Model modelToDelete)
        {
            var response = await client.DeleteAsync($"api/models/{modelToDelete.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

    }
}

