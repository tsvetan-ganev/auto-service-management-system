using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceManagementSystem.Models
{
	public class TroubleCode
	{
		public int TroubleCodeId { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public bool Permanent { get; set; }
	}
}
