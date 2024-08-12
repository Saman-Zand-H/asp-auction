using Auction.Data.Services;
using Auction.Models;
using Auction.Models.ViewModels;
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
        private readonly IListingService _listingService;

        public BidController(IBidService bidService, UserManager<IdentityUser> userManager, IListingService listingService)
        {
            _bidService = bidService;
            _userManager = userManager;
            _listingService = listingService;
        }

        [HttpPost("Create/{listingId}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(DetailsBidsVM bid, int listingId)
        {
            Listing? listing = await _listingService.GetById(listingId);
            if (listing == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }

            if (!ModelState.IsValid)
            {
                ListingDetailsVM vm = new()
                {
                    Bid = bid,
                    Listing = listing,
                };

                return View("~/Views/Listing/Details.cshtml", vm);
            }

            Bid newBid = new()
            {
                Price = bid.Price,
                ListingId = listingId,
                IdentityUserId = _userManager.GetUserId(User)
            };
            await _bidService.Add(newBid);
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