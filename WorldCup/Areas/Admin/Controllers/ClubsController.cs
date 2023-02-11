using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldCup.Areas.Admin.Data.Services;
using WorldCup.Areas.Admin.Models;
using WorldCup.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Data.ViewModels;
using WorldCup.Models;

namespace WorldCup.Areas.Admin.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Area("Admin")]
	public class ClubsController : Controller
	{
		private readonly IClubsService _clubsService;
		private readonly AppDbContext _context;
		private readonly IPhotoService _photoService;

		public ClubsController(IClubsService clubsService,AppDbContext context, IPhotoService photoService)
		{
			_clubsService= clubsService;
            _context= context;
            _photoService= photoService;    
		}
        [Route("Admin/Clubs/Index")]
        public async Task<IActionResult> Index()
        {
            var data = await _clubsService.GetAllAsync();
            return View(data);
        }

   
        [Route("Admin/Clubs/Create")]
        public IActionResult Create()
		{
			return View();
		}
     

        [Route("Admin/Clubs/Create/{id?}")]
        [HttpPost]
		public async Task<IActionResult> Create(ClubVM clubs)
		{
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubs.LogoUrl);
                var club = new Club
                {
                    Name = clubs.Name,
                    Birthday = clubs.Birthday,
                    LogoUrl = result.Url.ToString(),
                    State = clubs.State,
                    City = clubs.City,
                    Players = clubs.Players
                };
                await _clubsService.AddAsync(club);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
			return View(clubs);
		}


	
        [Route("Clubs/Details/{id?}")]
        public async Task<IActionResult> Details(int id)
        {
            var clubDetails = await _clubsService.GetByIdAsync(id);
            return View(clubDetails);
        }

        [Route("Clubs/Edit/{id?}")]
        public async Task<IActionResult> Edit(int id)
        {
            var clubDetails = await _clubsService.GetByIdAsync(id);
            if (clubDetails == null) return View("NotFound");
            return View(clubDetails);
        }

        [Route("Clubs/Edit/{id?}")]
         
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birthday,LogoUrl,State,City")] Club clubs)
        {

            if (!ModelState.IsValid)
            {
                return View(clubs);
            }
            await _clubsService.UpdateAsync(id, clubs);
            return RedirectToAction(nameof(Index));
        }

        [Route("Admin/Clubs/Delete/{id?}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubsService.GetByIdAsync(id);
            if (clubDetails == null) return View("NotFound");
            return View(clubDetails);
        }
        [Route("Admin/Clubs/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clubDetails = await _clubsService.GetByIdAsync(id);
            if (clubDetails == null) return View("NotFound");

            await _clubsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Route("Admin/Clubs/Sorting/{sorder?}/{search?}")]
        public async Task<IActionResult> Sorting(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
           
            var clubs = await _clubsService.GetAllAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                clubs = clubs.Where(p => p.Name.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "Name":
                    clubs = clubs.OrderBy(s => s.Name);
                    break;
         
                default:
                    clubs = clubs.OrderByDescending(s => s.Name);
                    break;
            }


            return View(clubs.ToList());
        }

        [Route("Admin/Clubs/CheckUsernameAvailability/{userdata?}")]
        public JsonResult CheckUsernameAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(200);


            var SeachData = _context.Clubs.Where(x => x.Name == userdata).SingleOrDefault();
            if (SeachData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }
    }
}
