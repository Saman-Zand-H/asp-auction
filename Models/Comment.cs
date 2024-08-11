using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Auction.Models
{
    public class Comment : IHasAuthor
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;

        public string? IdentityUserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public int? ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public bool IsUserAuthor(string userId)
        {
            return IdentityUserId == userId;
        }
    }
}
