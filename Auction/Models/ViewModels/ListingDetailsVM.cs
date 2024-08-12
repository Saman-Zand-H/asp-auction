namespace Auction.Models.ViewModels
{
    public class ListingDetailsVM
    {
        public Listing Listing { get; set; } = null!;
        public DetailsBidsVM Bid { get; set; } = new DetailsBidsVM();
        public DetailsCommentVM Comment { get; set; } = new DetailsCommentVM();
    }
}