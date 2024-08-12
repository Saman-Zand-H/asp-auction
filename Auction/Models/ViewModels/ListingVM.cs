using System.ComponentModel.DataAnnotations;

namespace Auction.Models.ViewModels
{
    public class ListingVM
    {
        [Required]
        [Display(Name = "Title: ")]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [Display(Name = "Description: ")]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Price: ")]
        [Range(0.01, 10000000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Image: ")]
        public IFormFile Image { get; set; } = null!;
    }
}