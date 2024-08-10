using Auction.Models;
using Microsoft.EntityFrameworkCore;
namespace Auction.Data.Services
{
    public class ListingService : IListingService
    {
        private readonly ApplicationDbContext _context;

        public ListingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Listing> GetAll()
        {
            return _context.Listings.Include(l => l.IdentityUser);
        }

        public async Task Add(Listing listing)
        {
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
        }
    }
}