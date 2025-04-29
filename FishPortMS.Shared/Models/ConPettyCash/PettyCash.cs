using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishPortMS.Shared.Models.Account;
using FishPortMS.Shared.Models.FishPort;

namespace FishPortMS.Shared.Models.ConPettyCash
{
    public class PettyCash
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]

        public decimal Amount { get; set; } = 0;

        public UserProfile UserProfile { get; set; }
        public int UserDetailsId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Consignacion Consignacion { get; set; }
        public Guid ConsignacionId { get; set; }
    }
}
