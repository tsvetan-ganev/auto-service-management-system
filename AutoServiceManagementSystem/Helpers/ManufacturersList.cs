using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.Helpers
{
	public static class ManufacturersList
	{
		private static List<string> _manufacturers = new List<string>()
		{
			"Acura", "Audi", "Alfa Romeo", "Aston Martin", "Bentley", "BMW", "Bugatti",
            "Cadillac", "Chevrolet", "Chrysler", "Citroen", "Dacia", "Daewoo",
            "Daihatsu", "Datsun", "Dodge", "Ferrari", "Fiat", "Honda", "Hummer", "Hyundai", "Infiniti",
            "Isuzu", "Iveco", "Jaguar", "Jeep", "Kia", "Lada", "Lamborghini", "Land Rover",
            "Lexus", "Lincoln", "Lotus", "Maserati", "Maybach", "Mazda", "Mercedes", "Mini",
            "Mitsubishi", "Nissan", "Opel", "Peugeot", "Pontiac", "Porsche", "Ram", "Renault",
            "Rolls Royce", "SAAB", "Scion", "Seat", "Shelby", "Skoda", "Smart", "Subaru", "Suzuki",
            "Tesla", "Toyota", "Volkswagen", "Volvo", "Vauxhall"
		};

		public static List<string> Manufacturers
		{
			get
			{
				return _manufacturers;
			}
			private set
			{
				_manufacturers = value;
			}
		}
	}
}