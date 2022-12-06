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
    }
}
