using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FishPortMS.Shared.DTOs.MasterProductDTO
{
    public class UpdateMasterInventory
    {
        public int Id { get; set; }

        [Required]
        public int Qty { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]

        public decimal SRP { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DCP { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WSP { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PreviousPrice { get; set; } = 0;
    }
}
