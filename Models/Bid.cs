using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Auction.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public string? IdentityUserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public int? ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

    }
}