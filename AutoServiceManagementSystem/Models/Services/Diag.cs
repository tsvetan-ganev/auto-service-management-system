using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoServiceManagementSystem.Contracts;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Models.Services
{
	public class Diag : IService
	{
		// fields
		private ICollection<TroubleCode> tdcs;

		// ctor
		public Diag()
		{
			tdcs = new List<TroubleCode>();
			Price = 0;
		}

		// properties
		public int DiagId { get; set; }
        public string Name { get; set; }
		public decimal Price { get; set; }
		public virtual ICollection<TroubleCode> TroubleCodes
		{
			get { return tdcs; }
			set { tdcs = value; }
		}

		public void Add(TroubleCode dtc)
		{
			if (dtc != null)
			{
				tdcs.Add(dtc);
			}
		}
		
	}
}
