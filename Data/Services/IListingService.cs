using Auction.Models;

namespace Auction.Data.Services
{
    public interface IListingService
    {
        IQueryable<Listing> GetAll();
        Task Add(Listing listing);
    }
}