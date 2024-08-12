using System.ComponentModel.DataAnnotations;

namespace Auction.Models.ViewModels
{
    public class DetailsCommentVM
    {
        [Required]
        [Display(Name = "Content: ")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Content { get; set; } = string.Empty;
    }
}