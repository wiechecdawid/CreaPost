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
        private IOwner _owner;
        private IArticleRepository _articleRepository;
        private IAuthorRepository _authorRepository;

        public HomeController(IArticleRepository articleRepository, IAuthorRepository authorRepository, IOwner owner)
        {
            _owner = owner;
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
        }
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Articles = _articleRepository.GetAll();
            model.Owner = _owner;
            return View(model);
        }
    }
}
