﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishPortMS.Shared.Models.Expenses
{
	public class VendorExpenseCategory
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
	}
}
