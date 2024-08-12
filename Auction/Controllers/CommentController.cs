using Auction.Data.Services;
using Auction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Auction.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<IdentityUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (comment.ListingId == null)
            {
                return RedirectToAction(nameof(Index), "Listing");
            }

            comment.IdentityUserId = _userManager.GetUserId(User);
            await _commentService.Add(comment);
            return RedirectToAction("Details", "Listing", new { id = comment.ListingId });
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