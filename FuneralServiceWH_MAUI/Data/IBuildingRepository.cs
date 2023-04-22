using FuneralServiceWH_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuneralServiceWH_MAUI.Data
{
    internal interface IBuildingRepository
    {
        Task<List<Building>> GetBuildings();
        Task<List<Building>> GetBuilding(int id);
        Task AddBuilding(Building building);
        Task UpdateBuilding(Building building);
        Task DeleteBuilding(Building building);
    }
}
