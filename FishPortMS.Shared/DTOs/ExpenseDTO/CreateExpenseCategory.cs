using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.DTOs.ExpenseDTO
{
    public class CreateExpenseCategory
    {
        [Required]
        public string Title { get; set; }
    }
}
