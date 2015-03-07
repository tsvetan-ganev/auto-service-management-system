using AutoServiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutoServiceManagementSystem.Helpers
{
    public static class CarSeeder
    {
        private static Random rand = new Random();

        // Imaginary car model names used for testing.
        private static string[] modelNames = 
        {
            "Lightning", "Blaze", "Elegance", "Flash", "Sports", "Winner", "Leader", "Magnum",
            "Jupiter", "Le Incredibile", "RTX 50", "Devil", "Daemon", "Femili", "Excellence",
            "Street", "Beauty", "Dragon", "Tornado", "Typhoon"
        };

        // Bulgarian region codes used in Plate Numbers. 
        // They represent the area where the vehicle was registered.
        private static string[] regionCodes = 
        {
            "Е", "А", "В", "ВТ", "ВН", "ВР", "ЕВ", "ТХ", "К", "КН", "ОВ", "М", "РА", "РК",
            "ЕН", "РВ", "РР", "Р", "СС", "СН", "СМ", "СО", "С", "СА", "СВ", "Т", "Х", "Х", "У"
        };

        // World Manufacturer Identificators - the first three digits in a VIN code.
        // They give information about the country of origin and the manufacturer.
        // Note: Some manufacturers have factories in more than one country and
        // some WMIs have changed through the years. In a more realistic scenario
        // each manufacturer must have a list of WMIs.
        private static Dictionary<string, string> worldManufacturerIds = new Dictionary<string, string>() 
        {
            {"Acura", "19U"}, {"Audi", "WAU"}, {"Alfa Romeo", "ZAR"}, {"Aston Martin", "SCF"}, {"Bentley", "SCB"},
            {"BMW", "WBA"},{"Bugatti", "ZA9"}, {"Cadillac", "1G6"}, {"Chevrolet", "1G1"}, {"Chrysler", "1A8"},
            {"Citroen", "VF7"}, {"Dacia", "UU1"}, {"Datsun", "JN1"}, {"Dodge", "1B7"}, {"Daewoo", "KLA"},
            {"Daihatsu", "JD1"}, {"Ferrari", "ZFF"}, {"Fiat", "ZFA"}, {"Honda", "JHM"}, {"Hummer", "5GT"},
            {"Hyundai", "KM8"}, {"Infiniti", "JNK"}, {"Isuzu", "4KL"}, {"Iveco", "ZCF"}, {"Jaguar", "SAJ"},
            {"Jeep", "1J4"}, {"Kia", "KNA"}, {"Lada", "XTA"}, {"Lamborghini", "ZA9"}, {"Land Rover", "SAL"},
            {"Lexus", "JT6"}, {"Lincoln", "1LN"}, {"Lotus", "SCC"}, {"Maserati", "ZAM"}, {"Maybach", "WDB"},
            {"Mazda", "JM1"}, {"Mercedes", "WDB"}, {"Mini", "WMW"}, {"Mitsubishi", "JA3"},{"Nissan", "JN3"},
            {"Opel", "W0L"}, {"Peugeot", "VF3"}, {"Pontiac", "1G2"}, {"Porsche", "WP0"}, {"Ram", "3B4"},
            {"Renault", "VF1"}, {"Rolls Royce", "SCA"}, {"SAAB", "YS3"}, {"Scion", "JTK"}, {"Seat", "VSS"},
            {"Shelby", "SFM"}, {"Skoda", "TMB"}, {"Smart", "WME"}, {"Subaru", "JF1"}, {"Suzuki", "JS2"}, 
            {"Tesla", "5YJ"}, {"Toyota", "JT2"}, {"Volkswagen", "WVW"}, {"Volvo", "YV1"}, {"Vauxhall", "VN1"}
        };

        // In a VIN code the 10th digit represents the year of manufacturing.
        private static Dictionary<char, int> charToYear = new Dictionary<char, int>()
        {
            {'L', 1990}, {'M', 1991}, {'N', 1992}, {'P', 1993}, {'R', 1994}, {'S', 1995}, {'T', 1996},
            {'V', 1997}, {'W', 1998}, {'X', 1999}, {'Y', 2000}, {'A', 2010}, {'1', 2001}, {'2', 2002},
            {'3', 2003}, {'4', 2004}, {'5', 2005}, {'6', 2006}, {'7', 2007}, {'8', 2008}, {'9', 2009},
            {'B', 2011}, {'C', 2012}, {'D', 2013}, {'E', 2014}, {'F', 2015}, {'G', 2016}, {'H', 2017},
            {'J', 2018}, {'K', 2018}
        };

        /// <summary>
        /// Selects a random manufacturer from a list
        /// of automotive manufacturers.
        /// </summary>
        /// <returns>Manufacturer name as a string.</returns>
        public static string GenerateManufacturer()
        {
            string[] manufacturers = ManufacturersList.Manufacturers.ToArray();
            string result = manufacturers[rand.Next(0, manufacturers.Length)];

            return result;
        }

        /// <summary>
        /// Selects a random model name from a list of imaginary model names.
        /// </summary>
        /// <returns>Model name as a string.</returns>
        public static string GenerateModel()
        {
            string result = modelNames[rand.Next(0, modelNames.Length)];
            return result;
        }

        /// <summary>
        /// Randomly generates a plate number according to the Bulgarian
        /// standard: X(X) NNNN Y(Y), where X is the region code,
        /// N is a digit and Y is a random combination of allowed characters.
        /// </summary>
        /// <returns>Plate number as a string.</returns>
        public static string GeneratePlateNumber()
        {
            string[] allowedCharacters = { "А", "В", "Е", "К", "М", "Н", "О", "Р", "С", "Т", "У", "Х" };
            var plateNumber = new StringBuilder();
            plateNumber.Append(regionCodes[rand.Next(0, regionCodes.Length)]);
            plateNumber.Append(" ");
            plateNumber.Append(rand.Next(1000, 9999));
            plateNumber.Append(" ");
            plateNumber.Append(
                allowedCharacters[rand.Next(0, allowedCharacters.Length)]);
            plateNumber.Append(
                allowedCharacters[rand.Next(0, allowedCharacters.Length)]);

            return plateNumber.ToString();
        }

        /// <summary>
        /// Randomly generates an imaginary engine code.
        /// The format is XXXX, where X belongs to [A-Z]
        /// </summary>
        /// <returns>Engine code as a string.</returns>
        public static string GenerateEngineCode()
        {
            var engineCode = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                // get the ASCII code [65->90 = (A-Z)]
                char letter = (char)rand.Next(65, 91);
                engineCode.Append(letter);
            }

            return engineCode.ToString();
        }

        /// <summary>
        /// Generates a random fake VIN code for a given Manufacturer.
        /// </summary>
        /// <remarks>
        /// This method's purpose is for testing only. None of the
        /// generated VINs are guaranteed to be actual valid VINs.
        /// </remarks>
        /// <param name="manufacturer">Manufacturer name.</param>
        /// <returns>Fake VIN code as string.</returns>
        public static string GenerateVinCode(string manufacturer)
        {
            string allowedChars = "ABCDEFGHJKLMNPRSTUVWXYZ1234567890";
            var vin = new StringBuilder();

            // Assigning the World Manufacturer Identification
            // as the first three characters in the VIN.
            vin.Append(worldManufacturerIds[manufacturer]);

            // Assigning the Vehicle Descriptor Section
            // as the next five characters in the VIN
            for (int i = 0; i < 5; i++)
            {
                vin.Append(
                     allowedChars[rand.Next(0, allowedChars.Length)]);
            }

            // Assigning the Check Digit (position 9) as 0 since an actual
            // test won't be performed.
            vin.Append(Convert.ToChar("0"));

            // Assigning the Model Year Digit (position 10)
            // U, Z and 0 must not be used!
            char modelYearDigit = '0';
            while (modelYearDigit == '0' || modelYearDigit == 'U'
                || modelYearDigit == 'Z')
            {
                modelYearDigit = allowedChars[rand.Next(0, allowedChars.Length)];
            }
            vin.Append(modelYearDigit);

            // Assigning the last seven digits which hold information about
            // the assembly plant, extra options added to the vehicle, 
            // Production Sequence Number etc. The format is manufacturer
            // specific, so we will pretend that this is the production
            // sequence number N which means that this vehicle is the 
            // N-th produced vehicle of that model
            for (int i = 0; i < 7; i++)
            {
                string digits = "0123456789";
                vin.Append(digits[rand.Next(0, digits.Length)]);
            }

            return vin.ToString();
        }

        /// <summary>
        /// Generates a model year by a given VIN code.
        /// </summary>
        /// <remarks>
        /// The 10-th digit in the VIN code represents the year
        /// of manufacturing. 
        /// The values are taken from the charToYear dictionary.
        /// </remarks>
        /// <param name="vin">VIN code.</param>
        /// <returns>Year as an integer.</returns>
        public static int GetYear(string vin)
        {
            return charToYear[vin[9]];
        }

        public static Car NextCar(ApplicationUser user = null, Customer owner = null)
        {
            var car = new Car();
            car.Manufacturer = GenerateManufacturer();
            car.Model = GenerateModel();
            car.VIN = GenerateVinCode(car.Manufacturer);
            car.PlateCode = GeneratePlateNumber();
            car.Year = GetYear(car.VIN);
            car.EngineCode = GenerateEngineCode();
			car.Jobs = new List<Job>();
            car.User = user;
            car.Customer = owner;

            return car;
        }
    }
}