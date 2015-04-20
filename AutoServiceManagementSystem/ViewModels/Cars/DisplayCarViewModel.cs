using AutoServiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoServiceManagementSystem.ViewModels.Cars
{
    public class DisplayCarViewModel
    {
        public int CarId { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        [Display(Name = "Plate Number")]
        public string PlateCode { get; set; }

        public string VIN { get; set; }

        [Display(Name = "Engine Code")]
        public string EngineCode { get; set; }

        public int? Year { get; set; }

        [Display(Name = "Fuel Type")]
        public Fuel FuelType { get; set; }

    }
}