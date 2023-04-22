using FuneralServiceWH_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuneralServiceWH_MAUI.Data
{
    internal interface ICasketRepository
    {
        Task<List<Casket>> GetCaskets();
        Task<Casket> GetCasket(int id);
        Task<List<Casket>> GetCasketsByBuilding(int BuildingID);
        Task AddCasket(Casket casket);
        Task UpdateCasket(Casket casket);
        Task DeleteCasket(Casket casket);
    }
}
