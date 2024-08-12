using Auction.Models;

namespace Auction.Data.Services
{
    public interface ICommentService
    {
        Task Add(Comment bid);
        Task Delete(int id);
        Task<Comment?> GetById(int id);
    }
}