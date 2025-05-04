using FishPortMS.Shared.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.MasterProductDTO
{
    public class ProductCarouselDTO
    {
        [Key]
        public int Id { get; set; }
        public string ProductImageURL { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/No-Image-Placeholder.svg/1665px-No-Image-Placeholder.svg.png";

        public int MasterProductId { get; set; }
    }
}
