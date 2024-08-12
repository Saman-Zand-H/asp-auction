using Auction.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.Data.Services
{
    public class BidService : IBidService
    {
        private readonly ApplicationDbContext _context;

        public BidService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Bid bid)
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var bid = await GetById(id);
            if (bid == null) return;
            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();
        }

        public async Task<Bid?> GetById(int id)
        {
            return await _context
                .Bids
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}