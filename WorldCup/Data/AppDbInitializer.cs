using Microsoft.AspNetCore.Identity;
using WorldCup.Data.Static;
using WorldCup.Models;

namespace WorldCup.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.Highlights.Any())
                {
                    context.Highlights.AddRange(new List<Highlights>()

                    {
                        new Highlights()
                        {
                            Title = "Argentina 2-0 Mexico",
                            Description = "Watch the Argentina v Mexico Group C highlights from the FIFA World Cup Qatar 2022.",
                            ImgUrl="../images/arg_mex.jpg",
                            VideoUrl="https://youtu.be/Rb6P2sdlJTE",
                            CreationDate=DateTime.Now,
                        },
                    new Highlights()
                        {
                            Title = "Serbia 2-3 Switzerland",
                            Description = "Watch the Serbia v Switzerland Group G highlights from the FIFA World Cup Qatar 2022. ",
                            ImgUrl="../images/ser_swi.jpg",
                            VideoUrl="https://youtu.be/PHegMuOT_t8",
                            CreationDate=DateTime.Now,
                        }
                    ,
                       new Highlights()
                        {
                            Title = "Netherlands 3-1 USA",
                            Description = "Watch the Netherlands v USA Round of 16 highlights from the FIFA World Cup Qatar 2022. ",
                            ImgUrl="../images/net_usa.jpg",
                            VideoUrl="https://youtu.be/4WGpIOwkLA4",
                            CreationDate=DateTime.Now,
                        }
                       ,
                        new Highlights()
                        {
                            Title = "Tunisia 1-0 France",
                            Description = "Watch the Tunisia v France highlights from the FIFA World Cup Qatar 2022. ",
                            ImgUrl="../images/tun_fra.jpg",
                            VideoUrl="https://youtu.be/CtYUqIrrm88",
                            CreationDate=DateTime.Now,
                        },
                         new Highlights()
                        {
                            Title = "Wales 3-0 England",
                            Description = "Watch the Wales v England Group B highlights from the FIFA World Cup Qatar 2022. ",
                            ImgUrl="../images/wal_eng.jpg",
                            VideoUrl="https://www.youtube.com/watch?v=EDUwOfAni38",
                            CreationDate=DateTime.Now,
                        },
                          new Highlights()
                        {
                            Title = "Japan 2-1 Spain",
                            Description = "Watch the Japan v England Group E highlights from the FIFA World Cup Qatar 2022. ",
                            ImgUrl="../images/jap_spa.jpg",
                            VideoUrl="https://www.youtube.com/watch?v=91eoGiLSCgY",
                            CreationDate=DateTime.Now,
                        },

                    });
                    context.SaveChanges();
                }
            }

        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Editor))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Editor));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@fifa.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "app-admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@fifa.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appEditorEmail = "editor@fifa.com";

                var appEditor = await userManager.FindByEmailAsync(appEditorEmail);
                if (appEditor == null)
                {
                    var newAppTeacher = new ApplicationUser()
                    {
                        FullName = "Application Editor",
                        UserName = "app-editor",
                        Email = appEditorEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppTeacher, "Editor@1234!");
                    await userManager.AddToRoleAsync(newAppTeacher, UserRoles.Editor);
                }
            }
        }
    }
}
