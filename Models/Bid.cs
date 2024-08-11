using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Auction.Models
{
    public class Bid : IHasAuthor
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public string? IdentityUserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public int? ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public bool IsUserAuthor(string userId)
        {
            return IdentityUserId == userId;
        }

        public bool IsWinner()
        {
            return Listing!.Price == Price;
        }

        public bool IsUserWinner(string userId)
        {
            return IsWinner() && IdentityUserId == userId;
        }
    }
}