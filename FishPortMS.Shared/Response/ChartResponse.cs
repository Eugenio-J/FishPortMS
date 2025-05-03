using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Response
{
    public class ChartResponse<T>
    {
        public T Data { get; set; }
        public List<decimal> Percentage { get; set; } = new List<decimal>();
        public string[]? XAxisLabel { get; set; }
    }
}
