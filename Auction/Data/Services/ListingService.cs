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

        public async Task<Listing?> GetById(int id)
        {
            return await _context
                .Listings
                .Include(l => l.IdentityUser)
                .Include(l => l.Bids!).ThenInclude(b => b.User)
                .Include(l => l.Comments!).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task Delete(int id)
        {
            var listing = await GetById(id);
            if (listing == null) return;
            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Listing listing)
        {
            _context.Listings.Update(listing);
            await _context.SaveChangesAsync();
        }
    }
}