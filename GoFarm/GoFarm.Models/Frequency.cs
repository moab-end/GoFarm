using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoFarm.Models
{
	public class Frequency
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name="Freqency Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name="Frequency Count")]
		public int FrequencyCounty { get; set; }

	}
}
