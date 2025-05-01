using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Products
{
    public class ProductCarousel
    {
        [Key]
        public int Id { get; set; }
        public string ProductImageURL { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/No-Image-Placeholder.svg/1665px-No-Image-Placeholder.svg.png";

        [JsonIgnore]
        public MasterProduct? MasterProduct { get; set; }
        public int MasterProductId { get; set; }
    }
}
