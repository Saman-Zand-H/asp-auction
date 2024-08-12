namespace Auction.Models
{
    public interface IHasAuthor
    {
        public bool IsUserAuthor(string userId);
    }
}