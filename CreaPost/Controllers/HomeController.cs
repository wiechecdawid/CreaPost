using CreaPost.Data;
using CreaPost.Models;
using CreaPost.Services;
using CreaPost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<StoreUser> _userManager;
        private CreaPostDbContext _context;
        private SignInManager<StoreUser> _signInManager;

        public HomeController(CreaPostDbContext context, SignInManager<StoreUser> signInManager, UserManager<StoreUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            ArticleRepository = new ArticleRepository(_context);
            Owner = new Owner();
            AuthorRepository = new AuthorRepository(_context);
        }

        public IAuthorRepository AuthorRepository { get; set; }
        public IOwner Owner { get; set; }
        public IArticleRepository ArticleRepository { get; set; }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                Articles = ArticleRepository.GetRecentArticles(),
                Owner = Owner
            };

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

        public IActionResult AuthorDetails(int id)
        {
            var model = new AuthorDetailViewModel
            {
                Author = AuthorRepository.Get(id),
                Articles = ArticleRepository.GetAll().ToList()
                            .Where(a => a.AuthorId == id)
            };

            if (model == null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string authorName;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var storeUser = _context.StoreUsers.FirstOrDefault(su => su.Id == user.Id);

            if(storeUser.FirstName!=null&&storeUser.LastName!=null)
            {
                authorName = storeUser.FirstName + " " + storeUser.LastName;
            }
            else
            {
                authorName = storeUser.Email;
            }

            var author = new Author
            {
                Name = authorName
            };

            var model = new CreateViewModel
            {
                Author = author
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            
            Author author;
            var authorCheck = _context.Authors.FirstOrDefault(a => a.Name == model.Author.Name);

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
                _context.Authors.ToList().Add(author);
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
            
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return RedirectToAction("Index", "AboutController");
        }
    }
}
