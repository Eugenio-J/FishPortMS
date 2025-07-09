using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ConsignacionDTO
{
    public class GetBranchNameId
    {
        public string? Id { get; set; }
        public string BranchName { get; set; } = string.Empty;
    }
}
