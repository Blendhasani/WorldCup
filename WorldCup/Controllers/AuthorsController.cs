using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _authorsService;
        public IAccountService _register;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthorsController(IAuthorsService authorsService ,IAccountService register, UserManager<ApplicationUser> userManager)
        {
            _authorsService = authorsService;
            _register = register;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var data = await _authorsService.GetAllAsync(page);
            return View(data);
        }

		//get
		public IActionResult Create()
		{
			return View();
		}
		//Get :authors/Create

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Name,Surname,Email,Password")] Author authors)
		{

			if (!ModelState.IsValid)
			{
				return View(authors);
			}
			var newauthors = new Author()
			{
				Name = authors.Name,
				Surname = authors.Surname,
				Email= authors.Email,
				Password = authors.Password,
			};
            await _register.RegisterAuthor(newauthors);
            await _authorsService.AddAsync(newauthors);

            return RedirectToAction(nameof(Index));
		}

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var authorsDetails = await _authorsService.GetByIdAsync(id);
            if (authorsDetails == null) return View("NotFound");
            return View(authorsDetails);
        }
        //authors/Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Surname")] Author authors)
        {
            if (!ModelState.IsValid)
            {
                return View(authors);
            }
          
            await _authorsService.UpdateAsync(id, authors);
            return RedirectToAction(nameof(Index));
        }


		//Get: authors/Delete
		public async Task<IActionResult> Delete(int id)
		{
			var authorsDetails = await _authorsService.GetByIdAsync(id);
			if (authorsDetails == null) return View("NotFound");
			return View(authorsDetails);
		}

		public async Task<IActionResult> Details(int id)
		{
			var authorsDetails = await _authorsService.GetByIdAsync(id);
			return View(authorsDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var authorsDetails = await _authorsService.GetByIdAsync(id);
			if (authorsDetails == null) return View("NotFound");

			await _authorsService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
