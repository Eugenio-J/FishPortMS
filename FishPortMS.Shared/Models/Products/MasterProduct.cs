using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Products
{
    public class MasterProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public Guid CreatedBy { get; set; }
        public Guid LastUpdateBy { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

        public ProductCategory? ProductCategory { get; set; }
        public int? ProductCategoryId { get; set; }

        public MasterInventory MasterInventory { get; set; }

        public List<ProductCarousel>? ProductCarousels { get; set; }

        [JsonIgnore]
        public List<ClientProduct>? ClientProducts { get; set; }
    }
}
