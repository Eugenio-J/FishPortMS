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

        [Required(ErrorMessage = "SKU field is required.")]
        public string SKU { get; set; } = string.Empty;

        [Required(ErrorMessage = "UOM field is required.")]
        public string UOM { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pkg Qty field is required.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PkgQnty { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PkgCost { get; set; }

        public bool IsActive { get; set; }

        public List<string> ProductImageURLs { get; set; } = new List<string>();
    }
}
