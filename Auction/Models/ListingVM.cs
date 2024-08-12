namespace Auction.Models
{
    public class ListingVM : Listing
    {
        public new string? ImagePath
        {
            get { return null; }
            set { }
        }

        public IFormFile Image { get; set; } = null!;
    }
}