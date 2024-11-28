using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.Models.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required.")]
        [StringLength(10, ErrorMessage = "Abbreviation cannot exceed 10 characters.")]
        public string Abrv { get; set; }

        [Required(ErrorMessage = "Please select a Vehicle Make.")]
        public int MakeId { get; set; }

        [ValidateNever]
        public string MakeName { get; set; }
    }
}
