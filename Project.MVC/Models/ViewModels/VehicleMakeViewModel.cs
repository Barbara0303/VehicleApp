using System.ComponentModel.DataAnnotations;

namespace Project.MVC.Models.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name must be betweem 2 and 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required.")]
        [StringLength(10,MinimumLength = 2, ErrorMessage = "Abbreviation must be between 2 and 10 characters.")]
        public string Abrv { get; set; }
    }
}
