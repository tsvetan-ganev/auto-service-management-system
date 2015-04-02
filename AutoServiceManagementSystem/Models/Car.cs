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

        // constructor
        public Car()
        {
            Jobs = new List<Job>();
            Customer = new Customer();
        }

        // properties
        public int CarId { get; set; }

        [Required()]
        public string Manufacturer { get; set; }

        [MaxLength(20)]
        public string Model { get; set; }

        /* Bulgarian plate codes are in the following format:
         * X(X) NNNN Y(Y) 
         * - X(X) is a district character code
         * - NNNN is a number
         * - Y(Y) is a series containing up to two chars
         * Note: Rich people tend to buy letter plates with
         * format outside of the standard, e.g. X(X) NNNNNNN 
         */
        [Display(Name = "Plate Code")]
        [StringLength(maximumLength: 12, MinimumLength = 8,
            ErrorMessage = "Plate codes consist of between 8 and 12 symbols.")]
        public string PlateCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        // TODO: improve on live error display
        [Vin]
        public string VIN
        {
            get { return vin; }
            set
            {
                vin = value.ToString().ToUpper();
            }
        }

        [DisallowSpecialCharacters(allowDigits: true)]
        [Display(Name = "Engine Code")]
        public string EngineCode { get; set; }

        [Range(1950, 2021)]
        public int? Year { get; set; }

        [Display(Name = "Fuel Type")]
        public Fuel? FuelType { get; set; }

        [Display(Name = "Owner")]
        public Customer Customer { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        // enums
        public enum Fuel
        {
            Petrol,
            Diesel,
            Gas
        }

    }
}
