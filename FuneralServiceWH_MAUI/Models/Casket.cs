using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FuneralServiceWH_MAUI.Models
{
    public class Casket : Auditable
    {
        public int Id { get; set; }

        [Display(Name = "Building")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Building as the Primary Warehouse.")]
        public int BuildingId { get; set; }
        public Building Building { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Model cannot be left blank.")]
        public int ModelID { get; set; }
        public Model Model { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "Manufacturer cannot be left blank.")]
        public int ManufacturerId { get;set; }
        public Manufacturer Manufacturer { get; set; }

        [Display(Name = "Color")]
        [Required(ErrorMessage = "Color cannot be left blank.")]
        public int ColorId { get; set; }
        public Color Color { get; set; }

        [Required(ErrorMessage = "You must enter the Date of Creation.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.Today;

        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
