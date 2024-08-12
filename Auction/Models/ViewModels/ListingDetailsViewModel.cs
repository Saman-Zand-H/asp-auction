namespace Auction.Models.ViewModels
{
    public class ListingDetailsViewModel
    {
        public Listing Listing { get; set; } = null!;
        public Bid Bid { get; set; } = new Bid();
        public Comment Comment { get; set; } = new Comment();
    }
}