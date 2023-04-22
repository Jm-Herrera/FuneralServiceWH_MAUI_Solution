using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FuneralServiceWH_MAUI.Models
{
    public class Building : Auditable
    {
        public int Id { get; set; }

        [Display(Name = "Building Name")]
        [Required(ErrorMessage = "Building name cannot be left blank.")]
        [StringLength(50, ErrorMessage = "Building name cannot be more than 50 characters long.")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address cannot be left blank.")]
        [StringLength(150, ErrorMessage = "Address cannot be more than 50 characters long.")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City cannot be left blank.")]
        [StringLength(50, ErrorMessage = "City cannot be more than 50 characters long.")]
        public string City { get; set; }

        [Display(Name = "Region")]
        [Required(ErrorMessage = "Region cannot be left blank.")]
        [StringLength(50, ErrorMessage = "Region cannot be more than 50 characters long.")]
        public string Region { get; set; }

        [StringLength(7, MinimumLength = 6)]
        [Required(ErrorMessage = "Postal Code cannot be left blank.")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$")]
        public string PostalCode { get; set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }
        public ICollection<Casket> Caskets { get; set; } 
        
    }
}
