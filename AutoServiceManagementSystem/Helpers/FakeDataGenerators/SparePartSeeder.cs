using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Helpers
{
	public class SparePartSeeder
	{
		#region Private Fields
		private static Random rand = new Random();

		private static readonly string[] sparePartNames =
		{
			"Alternator", "Alternator bearing", "Alternator bracket", "Alternator fan", "Battery", "Battery cable",
			"Battery plate", "Voltage regulator", "Ammeter", "Clinometer", "Dynamometer", "Fuel gauge", "Hydrometer",
			"Odometer", "Speedometer", "Tachometer", "Voltmeter", "Water temperature meter", "Coil wire", "Distributor",
			"Electronic timing controller", "Ignition box", "Ignition coil", "Ignition magneto",
			"Spark plug", "Fog light", "Halogen", "Headlight", "Interior light", "License plate lamb", "Side lighting", "Tail light", "Airbag sensors",
			"Automatic transmission speed sensor", "Camshaft position sensor", "Crankshaft position sensor", "Engine sensor", "Fuel level sensor",
			"Fuel pressure sensor", "Knock sensor", "Light sensor", "Oil level sensor", "Oil pressure sensor", "O2 sensor", "Mass flow sensor",
			"Starter", "Glowplug", "Door switch", "Ignition switch", "Steering column switch", "Thermostat", "A/C harness", "Engine harness",
			"Interior harness", "Main harness", "Floor harness", "Control harness", "Air bag control module", "Alarm", "Central locking system",
			"Chassis control computer","Ground strap", "Performance chip", "Performance monitor", "Cruise control computer", "Door contact",
			"Engine computer and management system", "Engine control unit", "Fuse", "Fuse box", "Transmission computer", "ABS", "Bleed nipple",
			"Brake backing plate", "Brake backing pad", "Brake disc", "Brake drum", "Brake pad", "Brake pedal", "Brake piston", "Brake pump",
			"Brake roll", "Brake roll", "Brake rotor", "Brake servo", "Brake shoe", "Brake warning light", "Caliper", "Hold-down springs", "Hose",
			"Brake booster hose", "Hydraulic booster unit", "Load-sensing valve", "Master cylinder", "Metering valve", "Park brake lever",
			"Pressure differential valve", "Proportioning valve", "Reservoir", "Shoe return spring", "Tyre", "Vacuum brake booster", "Wheel cylinder",
			"Wheel stud", "Engine", "Air duct", "Air intake housing", "Air intake manifold", "Camshaft", "Connecting rod", "Crank case", "Crank pulley",
			"Crankshaft", "Crankshaft oil seal", "Cylinder head", "Cylinder head cover", "Cylinder head gasket", "Distributor cap", "Drive belt",
			"Engine block", "Engine cradle", "Engine shake damper", "Engine valve", "Gudgeon pin", "Harmonic balancer", "Heater", "Mounting", "Piston",
			"Poppet valve", "PCV valve", "Pulley", "Rocker arm", "Starter motor", "Turbocharger", "Tappet", "Timing tape", "Timing belt", "Valve cover",
			"Water pump pulley", "Air blower", "Coolant hose", "Cooling fan", "Fan blade", "Fan clutch", "Radiator", "Water neck", "Water pipe",
			"Water tank", "Water pump", "Oil filter", "Oil pump", "Catalytic converter", "Exhaust clamp and bracket", "Exhaust flange gasket",
			"Exhaust gasket", "Exhaust manifold", "Exhaust pipe", "Muffler", "Spacer ring", "Air filter", "Carburetor", "Fuel pump", "Chocke cable",
			"EGR valve", "Fuel cooler", "Fuel filter", "Fuel injector", "Fuel pump", "Fuel pressure regulator", "Intake manifold", "Fuel rail",
			"LPG system", "Throttle body"
		};
		#endregion

		public static string GenerateSparePartName()
		{
			string result = sparePartNames[rand.Next(0, sparePartNames.Length)];
			return result;
		}

		public static Decimal GeneratePrice(int min, int max)
		{
			decimal result = (decimal)rand.Next(min, max) + (decimal)rand.NextDouble();
			return result;
		}

		public static int GenerateQuantity(int min, int max)
		{
			return rand.Next(min, max);
		}

		// TODO: Improve algorithm
		public static string GenerateOemCode()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < 8; i++)
			{
				sb.Append(rand.Next(0, 10));
			}
			sb.Append('-');
			for (int i = 0; i < 8; i++)
			{
				sb.Append(rand.Next(0, 10));
			}

			return sb.ToString();
		}

		public static SparePart NextSparePart(Supplier supplier = null,
			Job job = null)
		{
			var sparePart = new SparePart();
			sparePart.Name = GenerateSparePartName();
			sparePart.Code = GenerateOemCode();
			sparePart.Price = Decimal.Round(GeneratePrice(10, 250), 2);
			sparePart.Quantity = GenerateQuantity(1, 12);
			sparePart.Supplier = supplier;
			sparePart.Job = job;

			return sparePart;
		}
	}
}