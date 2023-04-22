using FuneralServiceWH_MAUI.Models;
using FuneralServiceWH_MAUI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Color = FuneralServiceWH_MAUI.Models.Color;

namespace FuneralServiceWH_MAUI.Data
{
    public class ColorRepository : IColorRepository
    {
        readonly HttpClient client = new HttpClient();

        public ColorRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Color>> GetColors()
        {
            var response = await client.GetAsync("api/colors");
            if (response.IsSuccessStatusCode)
            {
                List<Color> color = await response.Content.ReadAsAsync<List<Color>>();
                return color;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<Color> GetColor(int ID)
        {
            var response = await client.GetAsync($"api/colors/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Color color = await response.Content.ReadAsAsync<Color>();
                return color;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddColor(Color color)
        {
            var response = await client.PostAsJsonAsync("api/colors", color);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateColor(Color color)
        {
            var response = await client.PutAsJsonAsync($"api/colors/{color.Id}", color);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteColor(Color colorToDelete)
        {
            var response = await client.DeleteAsync($"api/colors/{colorToDelete.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }


    }
}
