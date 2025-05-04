using System.ComponentModel.DataAnnotations.Schema;

namespace FishPortMS.Shared.DTOs.MasterProductDTO
{
    public class GetMasterProduct
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public float Rating { get; set; }
        public int NumberOfRating { get; set; }
        public int TotalRating { get; set; }

        public string SKU { get; set; } = string.Empty;

        public string UOM { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PkgQnty { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal PkgCost { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategory { get; set; } = string.Empty;

        public int ProductBrandId { get; set; }
        public string? ProductBrand { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public GetMasterInventory MasterInventory { get; set; }

        public List<ProductCarouselDTO> ProductImageURLs { get; set; }
    }
}
