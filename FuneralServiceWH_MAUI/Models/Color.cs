using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FuneralServiceWH_MAUI.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name cannot be left blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessage = "Value cannot be left blank.")]
        [StringLength(7, ErrorMessage = "Color in HEX must 7 chars long Ex: #EDEADE")]
        public string Value {get; set; }
        public ICollection<Casket> Caskets { get; set; }


    }
}
