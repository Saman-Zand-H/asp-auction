using Auction.Data.Services;
using Auction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Controllers
{
    [Route("[controller]")]
    public class BidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public BidController(IBidService bidService, UserManager<IdentityUser> userManager, ILogger<BidController> logger)
        {
            _bidService = bidService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Bid bid)
        {
            int? listingId = bid.ListingId;
            if (listingId == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }

            bid.IdentityUserId = _userManager.GetUserId(User);
            await _bidService.Add(bid);
            return RedirectToAction("Details", "Listing", new { id = listingId });
        }

        [Route("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Bid? bid = await _bidService.GetById(id);
            if (bid == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }

            int? listingId = bid.ListingId;
            await _bidService.Delete(bid.Id);
            if (listingId == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }
            return RedirectToAction("Details", "Listing", new { id = listingId });
        }
    }
}