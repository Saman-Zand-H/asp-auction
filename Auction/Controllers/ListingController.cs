using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Auction.Models;
using Auction.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Auction.Models.ViewModels;


namespace Auction.Controllers
{
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private const int PageSize = 5;

        public ListingController(IListingService listingService, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _listingService = listingService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            var applicationDbContext = _listingService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(l => l.Title.Contains(searchString));
            }

            return View(await PaginatedList<Listing>.CreateAsync(applicationDbContext.AsNoTracking(), pageNumber ?? 1, PageSize));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingVM listingVM)
        {
            if (!ModelState.IsValid)
            {
                return View(listingVM);
            }

            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            string imageName = listingVM.Image.FileName;
            string filePath = Path.Combine(uploadDir, imageName);

            using var fs = new FileStream(filePath, FileMode.Create);
            await listingVM.Image.CopyToAsync(fs);

            Listing listing = new()
            {
                Title = listingVM.Title,
                Description = listingVM.Description,
                Price = listingVM.Price,
                ImagePath = imageName,
                IsSold = false,
                IdentityUserId = _userManager.GetUserId(User)
            };
            await _listingService.Add(listing);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _listingService.GetById(id.Value);
            if (listing == null)
            {
                return NotFound();
            }

            var model = new ListingDetailsVM
            {
                Listing = listing,
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var listing = await _listingService.GetById(id);
            if (listing == null || _userManager == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);
            if (userId == null || !listing.IsUserAuthor(userId))
            {
                return Forbid();
            }

            return View(listing);
        }

        [Route("Listing/Delete/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listing = await _listingService.GetById(id);
            if (listing == null || _userManager == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);
            if (userId == null || !listing.IsUserAuthor(userId))
            {
                return Forbid();
            }
            await _listingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [Route("Listing/{id}/Close")]
        [Authorize]
        public async Task<IActionResult> Close(int id)
        {
            var listing = await _listingService.GetById(id);
            if (listing == null || _userManager == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);
            if (userId == null || !listing.IsUserAuthor(userId))
            {
                return Forbid();
            }

            listing.IsSold = true;
            await _listingService.Update(listing);
            return RedirectToAction(nameof(Index));
        }
    }
}