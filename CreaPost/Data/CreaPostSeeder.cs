using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Data;
using CreaPost.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace CreaPost.Data
{
    public class CreaPostSeeder
    {
        private readonly CreaPostDbContext _context;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public CreaPostSeeder(CreaPostDbContext context, IHostingEnvironment env, UserManager<StoreUser> manager)
        {
            _context = context;
            _env = env;
            _userManager = manager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("wiechec.dawid@gmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Dawid",
                    LastName = "Wiecheć",
                    UserName = "wiechec.dawid@gmail.com",
                    Email = "wiechec.dawid@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssvv0rd!");
                if (result != IdentityResult.Success)
                {
                    new InvalidOperationException("Could not create new user in seeder");
                }
                await _context.SaveChangesAsync();
            }

            if (!_context.Authors.Any())
            {
                var authors = new List<Author>()
                {
                    new Author()
                    {
                        Name="Maryla Rodowicz"
                    },
                    new Author()
                    {
                        Name="Cristiano Ronaldo"
                    },
                    new Author()
                    {
                        Name = "Stafano DiCorrado"
                    }
                };
                _context.Authors.AddRange(authors);

                _context.SaveChanges();
            }
            //var articles = _context.Articles.ToList();
            if (!_context.Articles.Any())
            {
                var articles = new List<Article>()
                {
                    new Article()
                    {
                        AuthorId = _context.Authors.First().Id,
                        Title="Ale to już było",
                        Body="Ale to już było"+Environment.NewLine+"I nie wróci więcej",
                        //User = user
                    },
                    new Article()
                    {
                        AuthorId = _context.Authors.First().Id,
                        Title="Piękny cyganie",
                        Body="Mój piękny cyganie...",
                        //User = user
                    }
                };

                _context.Articles.AddRange(articles);

                _context.SaveChanges();
            }                        
        }
    }
}

