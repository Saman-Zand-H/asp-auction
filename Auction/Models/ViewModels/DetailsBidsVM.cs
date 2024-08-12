using System.ComponentModel.DataAnnotations;

namespace Auction.Models.ViewModels
{
    public class DetailsBidsVM
    {
        [Required]
        [Display(Name = "Bid: ")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid bid amount.")]
        public decimal Price { get; set; }
    }
}