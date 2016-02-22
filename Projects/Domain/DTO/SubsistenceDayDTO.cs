﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class SubsistenceDayDTO
	{
		[Required]
		public string Date { get; set; }

        public bool Breakfast { get; set; }
        public bool Dinner { get; set; }
        public bool Supper { get; set; }
        public decimal Amount { get; set; }
	}
}