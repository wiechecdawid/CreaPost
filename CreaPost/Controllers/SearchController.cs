using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Data;
using CreaPost.Models;
using CreaPost.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreaPost.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private CreaPostDbContext _context;

        public IAuthorRepository AuthorRepository { get; set; }
        public IArticleRepository ArticleRepository { get; set; }

        public SearchController(CreaPostDbContext context)
        {
            _context = context;
            ArticleRepository = new ArticleRepository(_context);
            AuthorRepository = new AuthorRepository(_context);
        }
        public IActionResult Authors()
        {
            var model = AuthorRepository.GetAll().OrderBy(a => a.Name);
            return View(model);
        }

        public IActionResult Articles()
        {
            var model = ArticleRepository.GetAll();

            foreach (var article in model)
            {
                article.Author = AuthorRepository.Get(article.AuthorId);
            }

            model.OrderBy(a => a.Title);

            return View(model);
        }
    }
}