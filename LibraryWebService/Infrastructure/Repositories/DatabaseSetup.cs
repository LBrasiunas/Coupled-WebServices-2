﻿using Infrastructure.Domains.Authors.Models;
using Infrastructure.Domains.Books.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories
{
    public class DatabaseSetup
    {
        public static void StartDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedDatabase(serviceScope.ServiceProvider.GetService<RepositoryContext>());
            }
        }

        public static void SeedDatabase(RepositoryContext context)
        {
            context.Database.Migrate();

            if (!context.Books.Any())
            {
                Console.WriteLine("No data found, seeding test data");
                context.Author.AddRange(
                    new Author
                    {
                        Name = "Berauto",
                        Surname = "We fix everything related to cars"
                    },
                    new Author
                    {
                        Name = "QuickFix",
                        Surname = "Fast and efficient car repairs"
                    },
                    new Author
                    {
                        Name = "TuneUp",
                        Surname = "Get your car running smoothly"
                    });
                context.SaveChanges();
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Regular car maintenance",
                        CreatedDate = new DateTime(2024, 3, 4),
                        Author = context.Author.First(x => x.Id == 1),
                        Description = "Changed all of the filters and also the engine oil.",
                        IsAvailable = true,
                        Isbn = "FirstEntry"
                    },
                    new Book
                    {
                        Title = "Engine rebuild",
                        CreatedDate = new DateTime(2024, 2, 21),
                        Author = context.Author.First(x => x.Id == 3),
                        Description = "Did an entire engine rebuild including rods, pistons, valves, camshaft and also the turbo.",
                        IsAvailable = true,
                        Isbn = "SecondEntry"
                    },
                    new Book
                    {
                        Title = "Engine power tuning",
                        CreatedDate = new DateTime(2024, 2, 27),
                        Author = context.Author.First(x => x.Id == 3),
                        Description = "Did an engine remap to increase the power and torque figures.",
                        IsAvailable = true,
                        Isbn = "ThirdEntry"
                    }); ;
                context.SaveChanges();
                //context.Author.AddRange(
                //    new Author
                //    {
                //        Name = "Kaya",
                //        Surname = "Larson"
                //    },
                //    new Author
                //    {
                //        Name = "Glenn",
                //        Surname = "Mcgrath"
                //    },
                //    new Author
                //    {
                //        Name = "Evelyn",
                //        Surname = "Woodard"
                //    });
                //context.SaveChanges();
                //context.Books.AddRange(
                //    new Book
                //    {
                //        Title = "Midnight in Chernobyl",
                //        CreatedDate = new DateTime(2019, 1, 1),
                //        Author = context.Author.Where(x => x.Id == 1).First(),
                //        Description = "Journalist Adam Higginbotham’s definitive, years-in-the-making account of the Chernobyl nuclear power plant disaster—and a powerful investigation into how propaganda, secrecy, and myth have obscured the true story of one of the twentieth century’s greatest disasters.",
                //        IsAvailable = true,
                //        UnavailableUntil = DateTime.Now,
                //        Isbn = "978-0-0870-4839-3"
                //    },
                //    new Book
                //    {
                //        Title = "Bring Me Back",
                //        CreatedDate = new DateTime(2019, 5, 21),
                //        Author = context.Author.Where(x => x.Id == 1).First(),
                //        Description = "The million-copy bestselling author returns with a breathtaking thriller – now with exclusive new chapters to see how the story could have ended.",
                //        IsAvailable = false,
                //        UnavailableUntil = DateTime.Now.AddMonths(2),
                //        Isbn = "978-6-8858-4218-8"
                //    },
                //    new Book
                //    {
                //        Title = "It",
                //        CreatedDate = new DateTime(2019, 1, 1),
                //        Author = context.Author.Where(x => x.Id == 2).First(),
                //        Description = "Stephen King’s terrifying, classic #1 New York Times bestseller, “a landmark in American literature” (Chicago Sun-Times)—about seven adults who return to their hometown to confront a nightmare they had first stumbled on as teenagers…an evil without a name: It.",
                //        IsAvailable = true,
                //        UnavailableUntil = DateTime.Now,
                //        Isbn = "978-8-2204-4142-1"
                //    }); ;
                //context.SaveChanges();
            }
        }
    }
}
