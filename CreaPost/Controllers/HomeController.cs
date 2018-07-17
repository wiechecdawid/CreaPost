using CreaPost.Data;
using CreaPost.Models;
using CreaPost.Services;
using CreaPost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Controllers
{
    public class HomeController : Controller 
    {
        private CreaPostDbContext _context;

        public HomeController(CreaPostDbContext context)
        {
            _context = context;
            ArticleRepository = new ArticleRepository(_context);
            Owner = new Owner();
            AuthorRepository = new AuthorRepository(_context);
        }

        public IAuthorRepository AuthorRepository { get; set; }
        public IOwner Owner { get; set; }
        public IArticleRepository ArticleRepository { get; set; }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Articles = ArticleRepository.GetAll();
            model.Owner = Owner;

            foreach (var article in model.Articles)
            {
                article.Author = AuthorRepository.Get(article.AuthorId);
            }

            return View(model);
        }
        
        public IActionResult ArticleDetails(int id)
        {
            var model = ArticleRepository.Get(id);
            if (model == null)
                return RedirectToAction(nameof(Index));
            model.Author = AuthorRepository.Get(model.AuthorId);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            
            Author author;
            var authorCheck = _context.Autors.FirstOrDefault(a => a.Name == model.Author.Name);

            if (authorCheck != null)
            {
                author = authorCheck;
            }
            else
            {
                author = new Author
                {
                    Name = model.Author.Name,
                    RegisterDate = DateTime.Now
                };

                author.Articles.ToList().Add(model.Article);
                _context.Autors.ToList().Add(author);
                _context.SaveChanges();
            }

            var article = new Article
            {
                Title = model.Article.Title,
                Author = author,
                ShortDescription = model.Article.Body.Split('.')[0],
                Body = model.Article.Body,
                Area = model.Article.Area
            };

            ArticleRepository.Add(article);
            _context.SaveChanges();


            return RedirectToAction(nameof(ArticleDetails), new { id = article.Id });
        }

        public IActionResult DeleteArticle(int id)
        {
            var removal = ArticleRepository.Get(id);
            _context.Articles.Remove(removal);

            //var authorId = removal.Author.Id;
            //var author = AuthorRepository.Get(authorId);
            //author.Articles.ToList().Remove(removal);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return RedirectToAction("Index", "AboutController");
        }
    }
}
