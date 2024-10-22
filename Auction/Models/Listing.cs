using Microsoft.AspNetCore.Identity;

namespace Auction.Models
{
    public class Listing : IHasAuthor
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = null!;
        public bool IsSold { get; set; } = false;
        public string? IdentityUserId { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }
        public virtual ICollection<Bid>? Bids { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

        public IdentityUser? GetWinner()
        {
            if (!IsSold) return null;
            if (Bids == null || Bids.Count <= 0) return null;
            return Bids!.OrderByDescending(b => b.Price).First().User;
        }

        public bool IsUserAuthor(string userId)
        {
            return IdentityUserId == userId || IdentityUserId == null;
        }
    }
}