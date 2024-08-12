using Auction.Data.Services;
using Auction.Models;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Auction.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IListingService _listingService;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<IdentityUser> userManager, IListingService listingService)
        {
            _listingService = listingService;
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost("Create/{listingId}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(DetailsCommentVM comment, int listingId)
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
                    Comment = comment,
                    Listing = listing,
                };
                return View("~/Views/Listing/Details.cshtml", vm);
            }

            Comment newComment = new()
            {
                Content = comment.Content,
                ListingId = comment.ListingId,
                IdentityUserId = _userManager.GetUserId(User)
            };
            await _commentService.Add(newComment);
            return RedirectToAction("Details", "Listing", new { id = listingId });
        }

        [Route("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetById(id);
            if (comment == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }
            int? listingId = comment.ListingId;
            if (listingId == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }

            await _commentService.Delete(id);
            return RedirectToAction("Details", "Listing", new { id = listingId });
        }
    }
}