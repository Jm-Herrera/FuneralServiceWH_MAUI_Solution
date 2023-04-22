using FuneralServiceWH_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuneralServiceWH_MAUI.Data
{
    internal interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetManufacturers();
        Task<Manufacturer> GetManufacturer(int id);
        Task AddManufacturer(Manufacturer manufacturer);
        Task UpdateManufacturer(Manufacturer manufacturer);
        Task DeleteManufacturer(Manufacturer manufacturer);
    }
}
