using FishPortMS.Shared.Models.FishPort;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Products
{
    public class ClientProduct
    {
        [Key]
        public int Id { get; set; }

        public MasterProduct MasterProduct { get; set; }
        public int MasterProductId { get; set; }

        public ClientInventory ClientInventory { get; set; }

        public Consignacion Consignacion { get; set; }
        public Guid ConsignacionId { get; set; }
    }
}
