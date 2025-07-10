using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FishPortMS.Shared.Models.Products;

namespace FishPortMS.Shared.DTOs.MasterProductDTO
{
    public class GetProductDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;

        public Guid CreatedBy { get; set; }
        public Guid LastUpdateBy { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public int ProductCategoryId { get; set; }
        public int? ProductBrandId { get; set; }
        public List<ProductCarousel>? ProductCarousels { get; set; }

    }
}
