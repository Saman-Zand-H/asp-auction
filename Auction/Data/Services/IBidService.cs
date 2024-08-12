using Auction.Models;

namespace Auction.Data.Services
{
    public interface IBidService
    {
        Task Add(Bid bid);
        Task Delete(int id);
        Task<Bid?> GetById(int id);
    }
}