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
                        Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis vitae eros eget libero sagittis euismod. Suspendisse gravida turpis eu ex faucibus lobortis pharetra vitae dolor. Ut interdum felis nunc. Nulla vitae ligula sit amet magna dictum tempor. Nullam ac libero bibendum, malesuada nisl ut, congue tellus. Sed ligula nunc, vestibulum et tortor eget, sodales fermentum orci. Maecenas fringilla urna a diam faucibus pellentesque et nec lorem. Nam sed aliquet ex, non laoreet mi. Morbi sed diam eget lectus auctor maximus eu ac nibh. Aliquam iaculis tincidunt turpis, ut hendrerit elit venenatis suscipit. Pellentesque ultrices ultrices euismod. Proin at egestas turpis."
                        +Environment.NewLine+
                        "Cras cursus scelerisque eleifend. In hac habitasse platea dictumst. Sed quis dignissim purus, eget ornare nulla. Sed nec nulla purus. Nam sed viverra ex. Integer sollicitudin risus in tempus bibendum. Duis magna neque, aliquet at diam eget, mollis pharetra ex. Maecenas non convallis magna. Morbi id finibus eros, vitae fermentum ante. Mauris in justo sed lacus cursus posuere. Donec a sem et justo tempus tristique. Mauris diam diam, auctor eu dapibus vitae, porta a lectus. Nulla facilisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nam libero nulla, cursus sollicitudin euismod vitae, sodales eu massa.",
                        //User = user
                    }
                };

                _context.Articles.AddRange(articles);

                _context.SaveChanges();
            }                        
        }
    }
}

