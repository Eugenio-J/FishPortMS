using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FishPortMS.Shared.DTOs.MasterProductDTO
{
    public class CreateMasterProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name field is required.")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "UOM field is required.")]
        public string UOM { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public List<string> ProductImageURLs { get; set; } = new List<string>();
    }
}
