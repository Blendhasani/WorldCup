using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using WorldCup.Areas.Admin.Models;
using WorldCup.Data.Static;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class AccountService :IAccountService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpPost]
        public async Task RegisterAuthor(Author author)
        {


            var newUser = new ApplicationUser()
            {
                FullName = author.Name,
                Email = author.Email,
                UserName = author.Email


            };

            var newUserResponse = await _userManager.CreateAsync(newUser, author.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.Editor);
            await _context.SaveChangesAsync();


            /*   return newUser;*/
        }
    }

}

