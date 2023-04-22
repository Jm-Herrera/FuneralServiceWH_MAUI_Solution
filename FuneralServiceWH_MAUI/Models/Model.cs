using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FuneralServiceWH_MAUI.Models
{
    public class Model
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name cannot be left blank.")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        public string Name { get; set; }
        public ICollection<Casket> Caskets { get; set; } 
    }
}
