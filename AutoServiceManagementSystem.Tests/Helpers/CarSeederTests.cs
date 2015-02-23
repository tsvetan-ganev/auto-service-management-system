using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoServiceManagementSystem.Helpers;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Tests
{
    [TestClass]
    public class CarSeederTests
    {
        [TestMethod]
        public void VinGeneratorShouldGenerateRandomVins()
        {
            var uniqueVins = new HashSet<string>();
            int count = 99999;
            for (int i = 0; i < count; i++)
            {
                string vin = CarSeeder.GenerateVinCode("Mercedes");
                Debug.WriteLine(vin);
                Assert.AreEqual(vin.Length, 17);
                uniqueVins.Add(vin);
            }

            Assert.AreEqual(count, uniqueVins.Count);
        }

        [TestMethod]
        public void VinGneratorShouldGenerateCorrectWorldManufacturerIdentifiersForAllCarMakes()
        {
            var manufacturersList = 
                AutoServiceManagementSystem.Helpers.ManufacturersList.Manufacturers;

            foreach (var manuf in manufacturersList)
            {
                string vin = CarSeeder.GenerateVinCode(manuf);
                Debug.WriteLine(vin);
                Assert.AreEqual(vin.Length, 17);
            }
        }

        [TestMethod]
        public void EngineCodeGeneratorShouldGenerateRandomCodes()
        {
            var uniqueEngineCodes = new HashSet<string>();
            int count = 9999;
            for (int i = 0; i < count; i++)
            {
                string code = CarSeeder.GenerateEngineCode();
                uniqueEngineCodes.Add(code);
            }
            Assert.IsTrue((float)(90/100)*count < uniqueEngineCodes.Count);
        }

        [TestMethod]
        public void GetYearShouldReturnACorrectYearAccordingToVIN()
        {
            Dictionary<char, int> charToYear = new Dictionary<char, int>()
            {
                {'L', 1990}, {'M', 1991}, {'N', 1992}, {'P', 1993}, {'R', 1994}, {'S', 1995}, {'T', 1996},
                {'V', 1997}, {'W', 1998}, {'X', 1999}, {'Y', 2000}, {'A', 2010}, {'1', 2001}, {'2', 2002},
                {'3', 2003}, {'4', 2004}, {'5', 2005}, {'6', 2006}, {'7', 2007}, {'8', 2008}, {'9', 2009},
                {'B', 2011}, {'C', 2012}, {'D', 2013}, {'E', 2014}, {'F', 2015}, {'G', 2016}, {'H', 2017},
                {'J', 2018}, {'K', 2018}
            };

            for (int i = 0; i < 9999; i++)
            {
                string vin = CarSeeder.GenerateVinCode(
                    CarSeeder.GenerateManufacturer());
                int year = CarSeeder.GetYear(vin);
                Assert.AreEqual(year, charToYear[vin[9]]);
            }
        }

        [TestMethod]
        public void PlateNumberGeneratorShouldGenerateRandomPlateNumbers()
        {
            var uniquePlateNumbers = new HashSet<string>();
            var count = 9999;

            for (int i = 0; i < count; i++)
            {
                string plateNumber = CarSeeder.GeneratePlateNumber();
                uniquePlateNumbers.Add(plateNumber);
            }
            Assert.IsTrue((float)(95/100)*count < uniquePlateNumbers.Count);
        }

        [TestMethod]
        public void NextCarShouldGenerateRandomCars()
        {
            var uniqueCars = new HashSet<Car>();
            int count = 1000;

            for (int i = 0; i < count; i++)
            {
                var car = CarSeeder.NextCar();
                //Debug.WriteLine(car.Manufacturer);
                //Debug.WriteLine(car.Model);
                //Debug.WriteLine(car.VIN);
                //Debug.WriteLine(car.Year);
                //Debug.WriteLine(car.PlateCode);
                //Debug.WriteLine(car.EngineCode);
                uniqueCars.Add(car);
            }

            Assert.AreEqual(count, uniqueCars.Count);
        }
    }
}
