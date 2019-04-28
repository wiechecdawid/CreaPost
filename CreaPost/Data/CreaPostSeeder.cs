using System;
using System.Collections.Generic;
using System.Linq;
using CreaPost.Data;
using CreaPost.Models;
using Microsoft.AspNetCore.Hosting;

namespace CreaPost.Data
{
    public class CreaPostSeeder
    {
        private readonly CreaPostDbContext _context;
        private readonly IHostingEnvironment _env;

        public CreaPostSeeder(CreaPostDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if(!_context.Authors.Any())
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
                        Body="Ale to już było"+Environment.NewLine+"I nie wróci więcej"
                    },
                    new Article()
                    {
                        AuthorId = _context.Authors.First().Id,
                        Title="Piękny cyganie",
                        Body="Mój piękny cyganie..."
                    }
                };

                _context.Articles.AddRange(articles);

                _context.SaveChanges();
            }

            

            if (!_context.StoreUsers.Any())
            {

            }
        }
    }
}

