using FuneralServiceWH_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = FuneralServiceWH_MAUI.Models.Color;

namespace FuneralServiceWH_MAUI.Data
{
    internal interface IColorRepository
    {
        Task<List<Color>> GetColors();
        Task<Color> GetColor(int id);
        Task AddColor(Color color);
        Task UpdateColor(Color color);
        Task DeleteColor(Color color);
    }
}
