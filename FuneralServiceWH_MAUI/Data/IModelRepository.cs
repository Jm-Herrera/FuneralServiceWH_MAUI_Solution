using FuneralServiceWH_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuneralServiceWH_MAUI.Data
{
    internal interface IModelRepository
    {
        Task<List<Model>> GetModels();
        Task<Model> GetModel(int id);
        Task AddModel(Model model);
        Task UpdateModel(Model model);
        Task DeleteModel(Model model);
    }
}
