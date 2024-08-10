using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Auction.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public string ImagePath { get; set; } = null!;
        public bool IsSold { get; set; }
        public string? IdentityUserId { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }
        public virtual ICollection<Bid>? Bids { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}