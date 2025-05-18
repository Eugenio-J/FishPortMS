using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.MasterProductDTO
{
    public class CreateProductBrandDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
