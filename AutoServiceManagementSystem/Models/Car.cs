using AutoServiceManagementSystem.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AutoServiceManagementSystem.Models
{
    public class Car
    {
		// fields
		private string vin;
		private int? year;
        private ICollection<Job> jobs;

        // constructor
        public Car()
        {
            Jobs = new List<Job>();
        }

        // properties
        public int CarId { get; set; }

		[Required()]
        public string Manufacturer { get; set; }

		[MaxLength(20)]
        public string Model { get; set; }

		[Display(Name="Plate Code")]
		[StringLength(maximumLength: 12, MinimumLength = 4,
			ErrorMessage="Plate codes consist of between 4 and 12 symbols.")]
		public string PlateCode { get; set; }

		[Required(AllowEmptyStrings = true)]
		// TODO: improve on live error display
		[Vin]
		[Display(Description = "Vehicle Identification Number")]
        public string VIN 
		{ 
			get { return vin; }
			set
			{
				/// TODO: Actual validation
				//if (value.Length != 17)
				//{
				//	throw new ArgumentOutOfRangeException(
				//		"VIN codes consist of exactly 17 symbols.");
				//}
				vin = value.ToString().ToUpper();
			}
		}

		[DisallowSpecialCharacters(allowDigits: true)]
		[Display(Name = "Engine Code")]
		public string EngineCode { get; set; }

		[Range(1950, 2015)]
        public int? Year 
		{ 
			get {return year;}
			set
			{
				if (value > DateTime.Now.Year || value < 1950)
				{
					throw new ArgumentOutOfRangeException(
						"The year of manufacturing must be larger than 1950 and less or equal than the current year.");
				}
				year = value;
			}
		}

		[Display(Name = "Fuel Type")]
        public Fuel? FuelType { get; set; }

		public int OwnerId { get; set; }
        public virtual Customer Owner { get; set; }

        public virtual ICollection<Job> Jobs
        {
            get { return this.jobs; }
            set { this.jobs = value; }
        }

		// enums
		public enum Fuel
		{
			Petrol,
			Diesel,
			Gas
		}

    }
}
