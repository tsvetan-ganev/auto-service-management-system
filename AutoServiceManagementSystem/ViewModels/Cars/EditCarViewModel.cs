using AutoServiceManagementSystem.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Cars
{
    public class EditCarViewModel
    {
        public int CarId { get; set; }

        [Required()]
        public string Manufacturer { get; set; }

        [MaxLength(20)]
        public string Model { get; set; }

        [Display(Name = "Plate Code")]
        [StringLength(maximumLength: 12, MinimumLength = 8,
            ErrorMessage = "Plate codes consist of between 8 and 12 symbols.")]
        public string PlateCode { get; set; }

        // TODO: improve on live error display
        [Vin]
        [Required]
        public string VIN { get; set;}

        [DisallowSpecialCharacters(allowDigits: true)]
        [Display(Name = "Engine Code")]
        public string EngineCode { get; set; }

        [Range(1950, 2021)]
        public int? Year { get; set; }

        [Display(Name = "Fuel Type")]
        public AutoServiceManagementSystem.Models.Car.Fuel FuelType { get; set; }
    }
}