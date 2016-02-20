﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class Currency
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(50)]
		public string Name { get; set; }
		[Required, MaxLength(10)]
		public string Code { get; set; }		
	}
}
